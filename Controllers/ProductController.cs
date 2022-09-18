using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using OurShop.Models;
using Microsoft.Extensions.Logging;

namespace OurShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IImage _image;
        private readonly IRating _rating;
        private readonly ILogger<ProductController> _logger;
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
        private readonly ILike _like;
        private readonly ISubscribeStatuse _subscribeStatuse;
        private readonly ISubscribe _subscribe;
        private readonly IResident _resident;
        private readonly IUserAccount _userAccount;
        private readonly IWhastapp _whastapp;
        private readonly IDelivery _delivery;
        private readonly IDeliveryStatuse _deliveryStatuse;
        private byte[] Data { get; set; }
        public int PageSize = 52;
        public ProductController(ILogger<ProductController> logger,IDeliveryStatuse deliveryStatuse,IDelivery delivery,IResident resident,IUserAccount userAccount,IWhastapp whastapp,IRating rating,ISubscribe subscribe, ISubscribeStatuse subscribeStatuse, ILike like,IBrand brand,IIteamDetail iteamDetail, IIteamStatuse iteamStatuse, ISupplier supplier, ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _rating = rating;
            this._logger = logger;
            _deliveryStatuse = deliveryStatuse;
            _delivery = delivery;
            _resident = resident;
            _userAccount = userAccount;
            _whastapp = whastapp;
             _subscribe = subscribe;
            _subscribeStatuse = subscribeStatuse;
            _subscribe = subscribe;
            _like = like;
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Home(int productPage = 1)
        {
            int PageSize = 16;
            Anelka();
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

            return View(new ProductsListViewModel
            {
                Product = combineTible
 .OrderByDescending(p => p.IteamModel.View)
 .Skip((productPage - 1) * PageSize)
 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = combineTible.Count()
                }
            });
            return View();
        }
        public async Task<SupplierModel> Product(string UserName)
        {
            SupplierModel Co = await _supplier.GetByEmailAsync(UserName);
            if (Co == null)
            {
                var naa = await _userAccount.GetByEmailAsync(UserName);
                if (naa == null)
                {
                    return Co;
                }
                else
                {
                    Co = await _supplier.GetByIdAsync(naa.SupplierId);
                    if (Co == null)
                    {
                        return Co;
                    }
                }

            }
            return Co;
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Whatsapp()
        {
            Anelka();
             var sup = await Product(User.Identity.Name);
                if (sup == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var whap = await _whastapp.GetBySupplIdAsync(sup.SupplierId);
                    if (whap != null)
                    {
                        return RedirectToAction("Whatsap", "Product");
                    }
                   
                }
            
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Whatsapp(WhastappModel model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                var sup = await Product(User.Identity.Name);
                if(sup==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var whap = await _whastapp.GetBySupplIdAsync(sup.SupplierId);
                    if (whap != null)
                    {
                        return RedirectToAction("Whatsap", "Product");
                    }
                    else
                    {
                        model.Phon = Tr(model.Phon);

                        model.SupplierId = sup.SupplierId;

                        await _whastapp.AddAsync(model);
                        ViewBag.Error = "whatsapp Number Added";
                    }
                }
            }
            return View(model);
        }
        public string Tr(string Phone)
        {
            var Phon = "";
            if(Phone.Length==11)
            {
                Phon = Phone;
            }
            else
            {
                string phone = Phone.Remove(0, 1);
                Phon = "27" + phone;
            }

            
            return Phon;
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Whatsap()
        {
            Anelka();


                var sup = await Product(User.Identity.Name);
                if (sup == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {              
                    var whap = await _whastapp.GetBySupplIdAsync(sup.SupplierId);
                    if (whap == null)
                    {
                        ViewBag.Error = "Somthing went wrong";
                    }
                       return View(whap);
                }          
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Whatsap(WhastappModel model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                var sup = await Product(User.Identity.Name);
                if (sup == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var whap = await _whastapp.GetBySupplIdAsync(sup.SupplierId);
                    if (whap == null)
                    {
                        ViewBag.Error = "Somthing went wrong";
                    }
                    else
                    {
                        model.SupplierId = sup.SupplierId;
                        model.Phon = Tr(model.Phon);

                        await _whastapp.UpdatAsync(model);
                        ViewBag.Error = "whatsapp Number Updated";
                    }
                }
            }
            return View(model);
        }
        private async Task ProductList()
        {
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
            ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
            // ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
        }
        public async Task<CategoryModel> GetCategoryById(Guid id)
        {
            CategoryModel Data = await _categoryResp.GetByIdAsync(id);
            return Data;
        }
   
        private async Task ProductDetailList()
        {
            ViewBag.SizeId = new SelectList(await _size.TabAsync(), "SizeId", "Size");
            ViewBag.ColorId = new SelectList(await _color.TabAsync(), "ColorId", "Color");
        }

        private async Task<bool> ItemMax(Guid SupId)
        {
            bool Is = false;
            IEnumerable<IteamModel> Item = await _iteam.TabGroupAsync(SupId);
            int count = Item.Count();
            if(count<5)
            {
                Is = true;
            }
            else
            {
                Is = false;
            }
            return Is;
        }
        private async Task<bool> ItemSub(Guid SupId,Guid Status)
        {
            bool Is = false;
            var Data = await _subscribe.GetBySupplierStatuseIdAsync(SupId, Status);
            if(Data==null)
            {
                Is = false;
            }
            else
            {
                Is = true;
            }
            return Is;
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Product(string Id,string id, int productPage = 1)
        {
            Anelka();
            IEnumerable<IteamModel> Item = await _iteam.TabAsync();
            if (Id!=null)
            {
                Guid i = new Guid(Id);
                Item = await _iteam.GetListItemByCategoryAsync(i);
            }
            else if (id != null)
            {
                Guid i = new Guid(id);
                var typ = await _categoryResp.DAsync(i);
                if(typ!=null)
                {
                    Item = await _iteam.GetListItemByCategoryAsync(typ.CategoryId);
                }
            }
            if(Item.Count()==0)
            {
                ViewBag.Empt = "The are Product for the select category yet";
            }
            IEnumerable<CategoryModel> Cat = await _categoryResp.TabAsync();
                    
                IEnumerable<GanderModel> Gand = await _gander.TabAsync();
                IEnumerable<IteamStatuseModel> Sta = await _iteamStatuse.TabAsync();
            ViewBag.Category = await _categoryResp.TabAsync();
            ViewBag.Brand = await _brand.TabAsync();
            ///ViewBag.Type = await _type.TabAsync();
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
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

            return View(new ProductsListViewModel
            {
                Product = combineTible
 .OrderByDescending(p => p.IteamModel.View)
 .Skip((productPage - 1) * PageSize)
 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = combineTible.Count()
                }
            });
            //           return View(combineTible.OrderBy(p => p.IteamModel.View)
            //.Skip((productPage - 1) * PageSize)
            //.Take(PageSize));
        }
        public async Task<IActionResult> Follow(string Id)
        {
            Anelka();
            RatingModel like = new RatingModel();
            if (User.Identity.Name != null)
            {
                Guid id = new Guid(Id);
                CustomerModel Cus = new CustomerModel();
                var Co = await _customer.GetByUserName(User.Identity.Name);
                if (Co == null)
                {
                    CustomerModel f = new CustomerModel();
                    f.CustomerUserName = User.Identity.Name;
                    var cc = await _customer.AddAsync(f);
                    Cus.CustomerId = cc.CustomerId;
                }
                else
                {
                    Cus.CustomerId = Co.CustomerId;
                }
                var Data = await _rating.GetCheckAsync(Cus.CustomerId, id);
                if (Data == null)
                {
                    like.Follow = true;
                    like.SupplierId = id;
                    like.CustomerId = Cus.CustomerId;
                    await _rating.AddAsync(like);
                }
                else
                {

                    if (Data.Follow == true)
                    {
                        await _rating.UpdCheckAsync(Data.CustomerId, Data.SupplierId, false);
                    }
                    else
                    {
                        await _rating.UpdCheckAsync(Data.CustomerId, Data.SupplierId, true);
                    }
                        
                }

            }
            return RedirectToAction("Page", "Product", new { Id = Id });
        }
        public async Task<bool> Foll(Guid id)
        {
            bool o = false;
            if (User.Identity.Name != null)
            {
                CustomerModel Cus = new CustomerModel();
                var Co = await _customer.GetByUserName(User.Identity.Name);
                if (Co == null)
                {
                    CustomerModel f = new CustomerModel();
                    f.CustomerUserName = User.Identity.Name;
                    var cc = await _customer.AddAsync(f);
                    Cus.CustomerId = cc.CustomerId;
                }
                else
                {
                    Cus.CustomerId = Co.CustomerId;
                }
                var Data = await _rating.GetCheckAsync( Cus.CustomerId,id);
                if(Data!=null)
                {
                    if (Data.Follow == true)
                    {
                        o = true;
                    }                 
                }
            }
            return o;
        }
        public async Task<bool> Like(string Id)
        {
            Anelka();
            bool Like = true;
            LikeModel like = new LikeModel();
            if (User.Identity.Name != null)
            {
                Guid id = new Guid(Id);
                CustomerModel Cus = new CustomerModel();
                var Co = await _customer.GetByUserName(User.Identity.Name);
                if (Co == null)
                {
                    CustomerModel f = new CustomerModel();
                    f.CustomerUserName = User.Identity.Name;
                    var cc = await _customer.AddAsync(f);
                    Cus.CustomerId = cc.CustomerId;
                }
                else
                {
                    Cus.CustomerId = Co.CustomerId;
                }
                var Data = await _like.CheckLikeAsync(id, Cus.CustomerId);
                if (Data == null)
                {
                    like.CustomerId = Cus.CustomerId;
                    like.IteamId = id;
                    like.Like = Like;
                    await _like.LikeAsync(like);
                }
            }

            return true;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(string Id,bool I)
        {
            Anelka();
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
                    if(I==true)
                    {
                        await Like(Id);
                    }
                 
                await  _iteam.View(Data.IteamId);
                    List<SizeModel> modelSize = new List<SizeModel>();
                    List<ColorModel> colorModel = new List<ColorModel>();
                    //Size
                    foreach (var size in await _size.TabAsync())
                    {
                        foreach (var Size in comp.Where(p=>p.Stock==true))
                        {
                           
                            if (Size.SizeId==size.SizeId)
                            {
                                modelSize.Add(size);
                                break;
                            }
                        }
                        
                    }

                  var Co= await _delivery.GetBySupplIdAsync(Data.SupplierId);
                    if(Co!=null)
                    {
                        if(Co.Active == true)
                        {
                            ViewBag.DeliveryDe = Co.DeliveryDe;
                            ViewBag.Location = Co.Location;
                            ViewBag.Price = Co.Price;
                            var op= await _deliveryStatuse.GetByIdAsync(Co.DeliveryStatuseId);
                            if (op != null)
                            {
                                ViewBag.Delivery = op.StatuseDelivery;
                            }
                            else
                            {
                                ViewBag.Delivery = "";
                            }
                           


                        }
                       else
                        {
                            ViewBag.Delivery = null;
                        }
                    }
                    else
                    {
                        ViewBag.Delivery = null;
                    }

                    foreach (var size in await _color.TabAsync())
                    {
                        foreach (var Size in comp.Where(p => p.Stock == true))
                        {

                            if (Size.ColorId == size.ColorId)
                            {
                                colorModel.Add(size);
                                break;
                            }
                        }

                    }
                    if (comp.Where(p=>p.Stock==true).Count()<=0)
                    {
                        ViewBag.Data = true;
                    }
                    ViewBag.SizeId = new SelectList(modelSize, "SizeId", "Size");
                    ViewBag.ColorId = new SelectList(colorModel, "ColorId", "Color");
                    //await ProductList();
                    var Category = await GetCategoryById(Data.CategoryId);
                    ViewBag.Category = Category.CategoryName;
                    ViewBag.CategoryId = Category.CategoryId;
                    var Gander = await _gander.TabAsync();
                    ViewBag.Phone = await What();
                    ViewBag.Gander = Gander.FirstOrDefault(o => o.GanderId == Data.GanderId);
                    return View(data);
                }
                else
                    return RedirectToAction("Product", "Product");
            }
            else
                return RedirectToAction("Product", "Product");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Page(string Id,string i)
        {
            Anelka();
            SupplierModel supplier;
            if (Id ==null&&i==null)
            {
                return RedirectToAction("Error", "Error");
            }
            else if(i!=null)
            {
                Guid d = new Guid(i);
                supplier = await _supplier.GetByBrandIdAsync(d);
            }
            else 
            {
                Guid id = new Guid(Id);
                supplier = await _supplier.GetByIdAsync(id);
            }
            
            if(supplier == null)
            {
                return RedirectToAction("Error", "Error");
            }
            var bra = await _brand.GetByIdAsync(supplier.BrandId);
            IEnumerable<IteamModel> Item = await _iteam.TabGroupAsync(supplier.SupplierId);
            int viewa = 0;
            int likes = 0;
            foreach (var tem in Item)
            {
                viewa = viewa + tem.View;
                var like = await _like.TabByItemUserAsync(tem.IteamId);
                likes = likes + like.Count();
            }

            var Location = await _resident.GetBySupplIdAsync(supplier.SupplierId);
            if(Location!=null)
            {
                ViewBag.Address1 = Location.Address1;
                ViewBag.Address2 = Location.Address2;
                ViewBag.Suburb = Location.Suburb;
                ViewBag.City = Location.City;
                ViewBag.PostCode = Location.PostCode;
            }


            var sup = await _rating.TabSupplierAsync(supplier.SupplierId);
            ViewBag.Phone= await What();
            ViewBag.Brand = bra.BrandName;
            ViewBag.Slogan = bra.BrandSlogn;
            ViewBag.Data = bra.BrandLogo;
            ViewBag.Like = likes;
            ViewBag.View = viewa;
            ViewBag.Follow = sup.Count();
            ViewBag.SupplierId = supplier.SupplierId;
            ViewBag.Follo = await Foll(supplier.SupplierId);
            ViewBag.Post = Item.Count();
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
        public async Task<string> What()
        {
            var sup = await Product(User.Identity.Name);
            if (sup == null)
            {
                return null;
            }

            var whap = await _whastapp.GetBySupplIdAsync(sup.SupplierId);
            if (whap == null)
            {
                return null;
            }

            return whap.Phon;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            Anelka();
            IEnumerable<BrandModel> Brand = await _brand.TabAsync();
           Guid ActiveStatuse = new Guid("EA45280F-8811-4D04-559B-08D963E7ACB0");
            ///Guid ActiveStatuse = new Guid("1DFF1EFD-66E2-470A-559C-08D963E7ACB0");
            List<PageModel> model = new List<PageModel>();
            IEnumerable<SupplierModel> Supp = await _supplier.TabSstatuseAsync(ActiveStatuse);
            foreach(var Data in Supp)
            {
                foreach(var data in Brand)
                {
                    if(Data.BrandId==data.BrandId)
                    {
                        PageModel mode = new PageModel();
                        var Item = await _iteam.TabGroupAsync(Data.SupplierId);
                        var bra = await _brand.GetByIdAsync(Data.BrandId);
                        var sup = await _rating.TabSupplierAsync(Data.SupplierId);
                         mode.Post = Item.Count();
                        mode.Follows = sup.Count();
                        mode.Follow = await Foll(Data.SupplierId);
                        mode.BrandName = bra.BrandName;
                        mode.BrandSlogan = bra.BrandSlogn;
                        mode.BrandLogo = bra.BrandLogo;
                        mode.SupplierId = Data.SupplierId;
                        int viewa = 0;
                        int likes = 0;
                        foreach (var tem in Item)
                        {
                            viewa= viewa+tem.View;
                            var like = await _like.TabByItemUserAsync(tem.IteamId);
                            likes = likes + like.Count();
                        }
                        mode.Views = viewa;
                        mode.Like = likes;
                        model.Add(mode);
                    }
                }
            }

            return View(model);
        }
    }
}
