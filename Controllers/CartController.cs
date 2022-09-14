using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OurShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IImage _image;
        private readonly ISize _size;
        private readonly IGander _gander;
        private readonly IColor _color;
        private readonly ICategory _categoryResp;
        private readonly IType _type;
        private readonly IIteam _iteam;
        private readonly ICustomer _customer;
        private readonly ISupplier _supplier;
        private readonly IIteamStatuse _iteamStatuse;
        private readonly IIteamDetail _iteamDetail;
        private Cart cart;
        public CartController(IIteamDetail iteamDetail,Cart cartService, IIteamStatuse iteamStatuse, ISupplier supplier, ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _iteamDetail = iteamDetail;
            _type = Typep;
            _categoryResp = categoryResp;
            _customer = customer;
            _image = image;
            _iteam = iteam;
            _iteamStatuse = iteamStatuse;
            _size = size;
            _gander = gander;
            _color = color;
            _supplier = supplier;
            _type = Typep;
            cart = cartService;

        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public async Task<RedirectToActionResult> AddToCart(IteamViewCData  data)
        {
            string returnUrl = data.returnUrl;
               if (data.No < 1)
               {
              
                return RedirectToAction("Index", new { returnUrl });//alert message product not add to chart
               }

               var value=await _iteamDetail.CheckAsync(data.id, data.ColorId, data.SizeId);
               if(value==null)
               {
                return RedirectToAction("Index", new { returnUrl });//alert message product not add to chart
               }
            IteamModel product = await _iteam.GetByIdAsync(data.id);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(value, data.No, data.ColorId, data.SizeId, product.Price);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpGet]
        public async Task<ActionResult> Details(string IteamId, string returnUrl)
        {
            CustomerDeailModel data = new CustomerDeailModel();
            Guid id = new Guid(IteamId);
            IteamModel product = await _iteam.GetByIdAsync(id);
            if (product != null)
            {

                data.images = await _image.GetListByItemIdsync(product.IteamId);
                data.item = product;
                data.iteamDetailModels = await _iteamDetail.GetListByItemIdsync(product.IteamId);
                await ProductDetailList();
                await ProductList();
                return View(data);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
      

        public async Task<RedirectToActionResult> RemoveFromCart(string Id, string returnUrl)
        {
            IteamDetailModel product = new IteamDetailModel();
            if (Id!=null)
            {
                Guid id = new Guid(Id);
               product = await _iteamDetail.GetById(id);
            }
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        private async Task ProductList()
        {
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
            ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
            ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
        }
        private async Task ProductDetailList()
        {
            ViewBag.SizeId = new SelectList(await _size.TabAsync(), "SizeId", "Size");
            ViewBag.ColorId = new SelectList(await _color.TabAsync(), "ColorId", "Color");
        }
    }
}

