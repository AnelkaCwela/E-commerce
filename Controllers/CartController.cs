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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
namespace OurShop.Controllers
{
    [AllowAnonymous]
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
        private readonly ILogger<CartController> _logger;
        private readonly IBrand _brand;
     private Cart cart;
        public CartController(ILogger<CartController> logger,IBrand brand,IIteamDetail iteamDetail,Cart cartService, IIteamStatuse iteamStatuse, ISupplier supplier, ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _brand = brand;
            _logger = logger;
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
        public void Anelka()
        {
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");

        }
        public async Task<ViewResult> Index(string returnUrl)
        {
            Anelka();
           await ProductDetailList();
            if(TempData["Data"]!=null)
            {
                ViewBag.Data = TempData["Data"].ToString();
                TempData.Remove("Data");
            }
            return View(new CartIndexViewModel
            {
                BrandTab = await _brand.TabAsync(),
                Cart = GetCart(),
                ReturnUrl = returnUrl,               
            }) ;
        }
        public async Task<string> GetCategoryById(Guid id)
        {
            var Data = await _categoryResp.GetByIdAsync(id);
            return Data.CategoryName;
        }
        [AllowAnonymous]
        public async Task<RedirectToActionResult> AddToCart(IteamViewCData  data)
        {
            Anelka();
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
                if (product == null)
                {
                 return RedirectToAction("Index", new { returnUrl });
                }
                Guid a = new Guid();
                Guid BraindId = await _supplier.GetBrandIdAsync(product.SupplierId);
               if(BraindId==a)
                {
                return RedirectToAction("Index", new { returnUrl });
                }

              var brand = await _brand.GetByIdAsync(BraindId);
              if (brand != null)
              {
                Cart cart = GetCart();
                int c = 0;
                var Clara = cart.Lines.FirstOrDefault(p => p.Iteam.IteamDetailId == value.IteamDetailId);
                if(Clara!=null)
                {
                     c = await Check(value.IteamDetailId, data.No, Clara.Quantity);
                  
                }
                else
                {
                     c = await Check(value.IteamDetailId, data.No);
                }
                           
                if(c>0)
                {
                    cart.AddItem(value, data.No, product, data.ColorId, data.SizeId, brand.BrandLogo, brand.BrandName);
                    SaveCart(cart);
                    c = c - data.No;
                }                               
                    string SMS = "The are Only -+ "+ c+" item Left , place your Oder";
                TempData["Data"] = SMS;              
            }
            return RedirectToAction("Index", new { returnUrl});
        }
        public async Task<int> Check(Guid IteamDetailId, int NoOfIteam)
        {
            var item = await _iteamDetail.GetById(IteamDetailId);

                if (item.NoOfIteam>=NoOfIteam)
                {
                return item.NoOfIteam;
                }
                else
                {
                    return 0;
                }

        }
        public async Task<int> Check(Guid IteamDetailId, int NoOfIteam,int left)
        {
            var item = await _iteamDetail.GetById(IteamDetailId);

            if(item.NoOfIteam>=NoOfIteam+left)
            {
                    return item.NoOfIteam;
            }
            else
            {
                return 0;
            }

        }

        public async Task<RedirectToActionResult> RemoveFromCart(string Id, string returnUrl)
        {
            Anelka();
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

