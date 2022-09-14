using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OurShop.Controllers
{
    public class ProductController : Controller
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
        private readonly IBrand _brand;
        private byte[] Data { get; set; }
        public ProductController(IBrand brand,IIteamDetail iteamDetail, IIteamStatuse iteamStatuse, ISupplier supplier, ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _brand = brand;
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
        }
        private async Task ProductList()
        {
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
            ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
            // ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
        }
        private async Task<string> GetCategoryById(Guid id)
        {
            var Data = await _categoryResp.GetByIdAsync(id);
            return Data.CategoryName;
        }
        private async Task ProductDetailList()
        {
            ViewBag.SizeId = new SelectList(await _size.TabAsync(), "SizeId", "Size");
            ViewBag.ColorId = new SelectList(await _color.TabAsync(), "ColorId", "Color");
        }
        //List of All Product to Customer
        [HttpGet]
        public async Task<IActionResult> Product()
        {
            IEnumerable<IteamModel> Item = await _iteam.TabAsync();
            IEnumerable<CategoryModel> Cat = await _categoryResp.TabAsync();
            IEnumerable<GanderModel> Gand = await _gander.TabAsync();
            IEnumerable<IteamStatuseModel> Sta = await _iteamStatuse.TabAsync();

            var combineTible = from c in Item
                               join ct in Gand on c.GanderId equals ct.GanderId into tab1
                               from ct in tab1.DefaultIfEmpty()
                               join xn24 in Sta on c.IteamStatuseId equals xn24.IteamStatuseId into tab24
                               from xn24 in tab24.DefaultIfEmpty()

                               join xn99 in Cat on c.CategoryId equals xn99.CategoryId into tab99
                               from xn99 in tab99.DefaultIfEmpty()

                               select new IteamViewModel
                               {
                                   Statuse = xn24,
                                   IteamModel = c,
                                   GanderModel = ct,
                                   CategoryModel = xn99
                               };

            return View(combineTible);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(string Id)
        {
            if (Id != null)
            {
                Guid id = new Guid(Id);
                var Data = await _iteam.GetByIdAsync(id);
                if (Data != null)
                {
                    IteamViewCData data = new IteamViewCData();
                    data.images = await _image.GetListByItemIdsync(Data.IteamId);
                    data.item = Data;
                 var comp=data.iteamDetailModels = await _iteamDetail.GetListByItemIdsync(Data.IteamId);
                    data.IteamModel = await _iteam.GetListItemByCategoryAsync(Data.CategoryId);

                    List<SizeModel> modelSize = new List<SizeModel>();
                    List<ColorModel> colorModel = new List<ColorModel>();
                    //Size
                    foreach (var size in await _size.TabAsync())
                    {
                        foreach (var Size in comp)
                        {
                           
                            if (Size.SizeId==size.SizeId)
                            {
                                modelSize.Add(size);
                                break;
                            }
                        }
                        
                    }
                    //Color
                    foreach (var size in await _color.TabAsync())
                    {
                        foreach (var Size in comp)
                        {

                            if (Size.ColorId == size.ColorId)
                            {
                                colorModel.Add(size);
                                break;
                            }
                        }

                    }
                    if(modelSize==null|| colorModel==null)
                    {
                        ViewBag.Data =true;
                    }


                    ViewBag.SizeId = new SelectList(modelSize, "SizeId", "Size");
                    ViewBag.ColorId = new SelectList(colorModel, "ColorId", "Color");
                    //await ProductList();
                    ViewBag.Category = await GetCategoryById(Data.CategoryId);
                    var Gander = await _gander.TabAsync();
                    ViewBag.Gander = Gander.FirstOrDefault(o => o.GanderId == Data.GanderId);
                    return View(data);
                }
                else
                    return RedirectToAction("Product", "Product");
            }
            else
                return RedirectToAction("Product", "Product");
        }


        //List Of All Online Store
        [HttpGet]
        public async Task<IActionResult> Page()
        {
            IEnumerable<BrandModel> Data = await _brand.TabAsync();
            return View(Data);
        }      
        //Profile of Page
       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BrandModel> Data = await _brand.TabAsync();
            return View(Data);
        }
        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }
    }
}
