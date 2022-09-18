using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OurShop.Models;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurShop.Controllers
{
    [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
    public class ManageController : Controller
    {
        private readonly IImage _image;
        private readonly ISize _size;
        private readonly IQoutationDetailz _qoutationDetailz;
        private readonly IQoutation _qoutation;
        private readonly IGander _gander;
        private readonly ISubscribeStatuse _subscribeStatuse;
        private readonly ISubscribe _subscribe;
        private readonly IColor _color;
        private readonly ICategory _categoryResp;
        private readonly IType _type;
        private readonly IIteam _iteam;
        private readonly ICustomer _customer;
        private readonly ISupplier _supplier;
        private readonly IIteamStatuse _iteamStatuse;
        private readonly IIteamDetail _iteamDetail;
        private readonly IUserAccount _userAccount;
      private readonly UserManager<UserPlusModel> _userManager;
        private byte[] DatAa { get; set; }
        public ManageController(IUserAccount userAccount,UserManager<UserPlusModel> userManager, IQoutation qoutation, IQoutationDetailz qoutationDetailz, ISubscribe subscribe, ISubscribeStatuse subscribeStatuse, IIteamDetail iteamDetail,IIteamStatuse iteamStatuse, ISupplier supplier, 
     ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _userAccount = userAccount;
            _iteamDetail = iteamDetail;
            _userManager = userManager;
            _type = Typep;
            _qoutationDetailz = qoutationDetailz;
            _qoutation = qoutation;
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
            _subscribe = subscribe;
            _subscribeStatuse = subscribeStatuse;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
            return View();
        }


        private async Task ProductList()
        {
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
            ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
           // ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
        }
        private async Task ProductDetailList()
        {
            ViewBag.SizeId = new SelectList(await _size.TabAsync(), "SizeId", "Size");
            ViewBag.ColorId = new SelectList(await _color.TabAsync(), "ColorId", "Color");
        }

        public async Task Update_StockDetail(Guid IteamDetailId,int NoOfIteam)
        {
               bool InStock = false;
               
                var item = await _iteamDetail.GetById(IteamDetailId);
                var Dat = await _iteam.GetByIdAsync(item.IteamId);
                if (item != null||Dat!=null)
                {
                    if(NoOfIteam>0)
                    {
                        InStock = true;
                        if(Dat.InStock==false)
                        {
                            await _iteam.UpdatStocksync(item.IteamId, InStock);
                        }
                    }
                    else
                    {
                        await _iteam.UpdatStocksync(item.IteamId, InStock);
                    }
                    
                    await _iteamDetail.UpdaItem(IteamDetailId, NoOfIteam, InStock);

                }
        }

        //Check out of Stock by sizeor coloe and return there number

        public async Task<int> CheckStockByDetail_No_Of_Itemw(string IteamDetailId)
        {
            int Count = 0;
            if (IteamDetailId != null)
            {
                Guid id = new Guid(IteamDetailId);
                var item = await _iteamDetail.GetById(id);
                if (item != null)
                {
                    Count = item.NoOfIteam;
                }
            }
            return Count;
        }
        //Check product if out of stock
        public async Task<bool> CheckStock(string IteamId)
        {
            bool Stock = false;
            if (IteamId != null)
            {
                Guid id = new Guid(IteamId);
                var item = await _iteam.GetByIdAsync(id);
                if (item != null)
                {
                    IEnumerable<IteamDetailModel> list = await _iteamDetail.GetListByItemIdsync(item.IteamId);

                    foreach (var Data in list)
                    {
                        if (Data.NoOfIteam > 0)
                        {
                            Stock = true;
                        }
                    }
                }
            }
            return Stock;
        }
        private async Task<int> CheckStock_No_Item(string IteamId)
        {
            int Count = 0;
            if (IteamId!=null)
            {
                Guid id = new Guid(IteamId);
                var item = await _iteam.GetByIdAsync(id);
                if(item!=null)
                {
                   
                   IEnumerable<IteamDetailModel> list = await _iteamDetail.GetListByItemIdsync(item.IteamId);

                    foreach(var Data in list)
                    {
                        if(Data.NoOfIteam>0)
                        {
                            Count++;
                        }
                    }
                }
            }
            return Count;
        }

        public async Task<JsonResult> GetTypeByIdd(Guid id)
        {
            List<CategoryModel> Cate = new List<CategoryModel>();
            Cate = ( await _categoryResp.ListByIdAsync(id)).ToList();
           // Cate.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "Select" });
            return Json(new SelectList(Cate, "CategoryId", "CategoryName"));

        }

        public async Task<JsonResult> GetTypeById(Guid id)
        {
            SelectList Data = new SelectList(await _categoryResp.ListByIdAsync(id),"CategoryId", "CategoryName");
            return Json(Data);
        }
        public async Task<JsonResult> ColorById(Guid id,string item)
        {
            SelectList Data = new SelectList(await _categoryResp.ListByIdAsync(id), "CategoryId", "CategoryName");
            return Json(Data);
        }
        public async Task<JsonResult> SizeById(Guid id)
        {
            SelectList Data = new SelectList(await _categoryResp.ListByIdAsync(id), "CategoryId", "CategoryName");
            return Json(Data);
        }
        private async Task<bool> ItemPri(Guid SupId)
        {
            bool Is = false;
            IEnumerable<IteamModel> Item = await _iteam.TabGroupAsync(SupId);
            int count = Item.Count();
            if (count <=50)
            {
                Is = true;
            }
            else
            {
                Is = false;
            }
            return Is;
        }
        private async Task<bool> ItemMax(Guid SupId)
        {
            bool Is = false;
            IEnumerable<IteamModel> Item = await _iteam.TabGroupAsync(SupId);
            int count = Item.Count();
            if (count <=5)
            {
                Is = true;
            }
            else
            {
                Is = false;
            }
            return Is;
        }
        private async Task<bool> ItemSub(Guid SupId, Guid Status,string UserName)
        {
            bool Is = false;
            var Data = await _subscribe.GetBySupplierStatuseIdAsync(SupId, Status);

            if (Data == null)
            {
                Is = false;
            }
            else
            {
                var userName = await _userManager.FindByEmailAsync(UserName);
                if(userName==null)
                {
                    Is = false;
                }
                else
                {
                    string Role = "Stand Plan";
                    var oo= await _userManager.AddToRoleAsync(userName, Role);
                    if(oo!=null)
                    {
                        Is = true;
                    }
                }
               
            }
            return Is;
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
        public async Task<IActionResult> Product()
        {
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if(Co==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                
                var ite= await ItemMax(Co.SupplierId);
                if (ite==false)
                {
                    Guid Id = new Guid("C80874A1-005B-4658-E9A6-08D97ADC4CFF");
                    bool Sub = await ItemSub(Co.SupplierId, Id, Co.SupplierUserName);
                    if (Sub == false)
                    {
                        TempData["Data"] = "Data";
                        return RedirectToAction("Plan", "Home");
                    }
                    else
                    {
                            var pri = await ItemPri(Co.SupplierId);

                            if (pri == false)
                            {
                              TempData["Prim"] = "Prime";
                            return RedirectToAction("Plan", "Home");
                            }                        

                    }
                }


                await ProductList();
                return View();
            }
            return RedirectToAction("Error", "Error");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Product(IteamDatamodel iteamModel)//Insert Product in the Database
        {
            IteamModel data = new IteamModel();
            IteamModel Model = new IteamModel();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Data = await Product(User.Identity.Name);                   
                    if (Data == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        Guid Statuse_Poduct = new Guid("38963C0F-E4EA-4187-6B53-08D963F49A8C");//Not In Sale
                        Model.ItermDate = DateTime.Now.Date;
                        Model.DiscountPrice = 0;
                        Model.IteamStatuseId = Statuse_Poduct;
                        Model.SupplierId = Data.SupplierId;
                        Model.CategoryId = iteamModel.CategoryId;                      
                        Model.GanderId = iteamModel.GanderId;
                        Model.ItermName = iteamModel.ItermName;        
                        Model.Price = iteamModel.Price;
                        Model.InStock = false;
                        int count = 1;
                        foreach (var file in Request.Form.Files)
                        {

                           
                            if(count==1&&file==null)
                            {
                                ViewBag.file = "png or jpn file format required";
                               await ProductList();
                                
                                return View(iteamModel);
                            }
                            MemoryStream ms = new MemoryStream();
                          
                            try
                            {
                                file.CopyTo(ms);
                                //check the file type selected if is png/jpn
                                var supported = new[] { "pjpeg", "svg", "pjp", "jfif","png", "jpg", "jpeg", "JPG" ,"PNG","JPEG"};
                                var f = Path.GetExtension(file.FileName).Substring(1);
                                if (!supported.Contains(f))
                                {

                                    // ModelState.AddModelError("","png or jpn file format required");
                                    ViewBag.file = "png or jpn file format required";
                                  await  ProductList();
                                   
                                    return View(iteamModel);
                                }
                                else
                                {
                                    DatAa = ms.ToArray();
                                }
                            }
                            catch
                            {
                                return RedirectToAction("Error", "Home");
                            }
                            if (count == 1)
                            {
                                Model.ImageIteam = ms.ToArray();
                            }
                            count++;
                            ms.Close();
                            ms.Dispose();
                            break;

                        }

                        data = await _iteam.AddAsync(Model);

                        if (data != null)
                        {

                            foreach (var file in Request.Form.Files)
                            {
                                MemoryStream ms = new MemoryStream();

                              
                                if (file != null)
                                {
                                    try
                                    {
                                        file.CopyTo(ms);

                                        //check the file type selected if is png/jpn
                                        var supported = new[] { "pjpeg", "svg", "pjp", "jfif", "png", "jpg", "jpeg", "JPG", "PNG", "JPEG" };
                                        var f = Path.GetExtension(file.FileName).Substring(1);
                                        if (supported.Contains(f))
                                        {

                                            ImageModel imageModel = new ImageModel();
                                            imageModel.IteamId = data.IteamId;
                                            imageModel.ImageData = ms.ToArray();
                                            await _image.AddAsync(imageModel);
                                        }

                                    }
                                    catch
                                    {
                                        return RedirectToAction("Error", "Error");
                                    }
                                }
                                
                              
                            }
                            //IteamDetaiInsertModel iteam = new IteamDetaiInsertModel();
                            string Id = data.IteamId.ToString();
                            return  RedirectToAction("Detail", "Manage",new { Id=Id});
                           

                        }
                        else
                         return   RedirectToAction("NotFound", "Error");
                    }
                }
                else
                    return RedirectToAction("NotFound", "Error");
            }
          await  ProductList();
            ViewBag.file = "png or jpn file format required";

            return View(iteamModel);
        }

        [HttpGet]
        [Route("Manage/Detail/{Id}")]
        public async Task<IActionResult> Detail(string Id)
        {

            if (Id != null)
            {
                Guid ItemId = new Guid(Id);
                var Data = await _iteam.GetByIdAsync(ItemId);
                if (Data != null)
                {
                    IteamDetaiInsertModel iteam = new IteamDetaiInsertModel();
                    iteam.CategoryId = Data.CategoryId;
                    iteam.GanderId = Data.GanderId;
                    iteam.ItermName = Data.ItermName;
                    iteam.Price = Data.Price;
                    iteam.ImageData = Data.ImageIteam;
                    iteam.IteamId = Data.IteamId;
                    iteam.IteamId = Data.IteamId;
                    iteam.Price = Data.Price;
                     await ProductDetailList();
                    await ProductList();
                    ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
                    return View(iteam);
                }
                else
                    return RedirectToAction("Error", "Error");
            }
            else
                return RedirectToAction("Error", "Error");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Detail(IteamDetaiInsertModel Data)
        {
            Guid id = new Guid();
            if (Data.IteamId == id)
            {
                return RedirectToAction("Error", "Error");
            }
            if (Data.ColorId == id)
            {
                await ProductDetailList();
                await ProductList();
                return View(Data);
            }
            if (Data.SizeId == id)
            {
                await ProductDetailList();
                await ProductList();
                return View(Data);
            }
            else
            {
                IteamDetailModel iteam = new IteamDetailModel();
                iteam.IteamId = Data.IteamId;
                iteam.NoOfIteam = Data.NoOfIteam;
                iteam.ColorId = Data.ColorId;
                iteam.SizeId = Data.SizeId;
       
                try
                {
                var de= await _iteamDetail.Add(iteam);
                    if(de!=null)
                    {
                        try
                        {
                            await Update_StockDetail(de.IteamDetailId, de.NoOfIteam);
                        }
                        catch
                        {
                            return RedirectToAction("Error", "Error");
                        }
                      //  IteamDetaiInsertModel item = new IteamDetaiInsertModel();

                        string Id = iteam.IteamId.ToString();
                        return RedirectToAction("Item","Manage", new { Id= Id });
                    }

                }
                catch
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }
            ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
            await  ProductDetailList();
           await ProductList();
            return View(Data);
        }
        [HttpGet]
        public async Task<IActionResult> Instock()
        {

            if (User.Identity.Name != null)
            {

                var Data = await Product(User.Identity.Name);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }


                IEnumerable<IteamModel> Item = await _iteam.TabGroupInstockAsync(Data.SupplierId,true);
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
                    if (TempData["Data"] != null)
                    {
                        ViewBag.Data = TempData["Data"];
                    }

                    return View(combineTible);

                
            }
            else
                return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> OutStock()
        {

            if (User.Identity.Name != null)
            {

                var Data = await Product(User.Identity.Name);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }

                IEnumerable<IteamModel> Item = await _iteam.TabGroupInstockAsync(Data.SupplierId, false);
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
                    if (TempData["Data"] != null)
                    {
                        ViewBag.Data = TempData["Data"];
                    }

                    return View(combineTible);

               
            }
            else
                return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {

            if (User.Identity.Name != null)
            {

                var Data = await Product(User.Identity.Name);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }

                IEnumerable<IteamModel> Item = await _iteam.TabGroupAsync(Data.SupplierId);
                    IEnumerable<CategoryModel> Cat = await _categoryResp.TabAsync();
                    IEnumerable<GanderModel> Gand = await _gander.TabAsync();
                    IEnumerable<IteamStatuseModel> Sta = await  _iteamStatuse.TabAsync();

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
                    if(TempData["Data"]!=null)
                    {
                        ViewBag.Data = TempData["Data"];
                    }

                    return View(combineTible);

                
            }
            else
                return RedirectToAction("Error", "Error");
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(IteamDetailUpdateData model)
        {

            if (ModelState.IsValid)
            {
                IteamDetailModel data = new IteamDetailModel();
               
                if(model.NoOfIteam >= 1)
                {
                    data.Stock = true;
                }
                else
                {
                    data.Stock = false;
                    var Data = await _iteamDetail.GetListByIteStockIdsync(model.item.IteamId, true);
                    if (Data == null)
                    {
                        await _iteam.UpdatStocksync(model.item.IteamId, false);
                    }
                }
                   
                     data.IteamDetailId = model.IteamDetailId;
                    data.NoOfIteam = model.NoOfIteam;
                    data.SizeId = model.SizeId;
                    data.ColorId = model.ColorId;
                 data.IteamId = data.IteamId;
                    await _iteamDetail.Upda(data);

                string Id = model.item.IteamId.ToString();
                return RedirectToAction( "Item", "Manage", new { Id = Id });

            }
            else
                return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> RemovePhoto(string I, string id)
        {
            if (I != null || id != null)
            {
                Guid Iditem = new Guid(id);
                Guid Iddetail = new Guid(I);
                var Data = await _iteam.GetByIdAsync(Iditem);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {

                    await _image.RemoveAsync(Iddetail);
                    string Id = Data.IteamId.ToString();


                    return RedirectToAction("Item", "Manage", new { Id = Id });
                }

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Image(string I)
        {
            if (I != null)
            {
                Guid Iddetail = new Guid(I);
                var Data = await _iteam.GetByIdAsync(Iddetail);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {

                    IEnumerable<ImageModel> mode = await _image.GetListByItemIdsync(Data.IteamId);

                    return View(mode);
                }

            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhoto(string oi,string u)
        {
            if (oi!= null&& u!=null)
            {
                Guid Iditem = new Guid(oi);
                

                    await _image.RemoveAsync(Iditem);

                    return RedirectToAction("Image", "Manage",new { I= u });
                

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Upload(string Id)
        {
           if(Id!=null)
            {
                Guid id = new Guid(Id);
                var Data = await _iteam.GetByIdAsync(id);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    ImageInsertModel model = new ImageInsertModel();
                    model.ItermName = Data.ItermName;
                    model.Price = Data.Price;
                    model.IteamId = Data.IteamId;
                    model.GanderId = Data.GanderId;
                    model.CategoryId = Data.CategoryId;
                    model.ImageData = Data.ImageIteam;
                    ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
                    ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
                    return View(model);
                }

            }
            return RedirectToAction("Error", "Error");

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upload(ImageInsertModel model)
        {
            if (model != null)
            {
                var Data = await _iteam.GetByIdAsync(model.IteamId);
                if (Data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();


                    if (file != null)
                    {
                        try
                        {
                            file.CopyTo(ms);

                            //check the file type selected if is png/jpn
                            var supported = new[] { "pjpeg", "svg", "pjp", "jfif", "png", "jpg", "jpeg", "JPG", "PNG", "JPEG" };
                            var f = Path.GetExtension(file.FileName).Substring(1);
                            if (supported.Contains(f))
                            {

                                ImageModel imageModel = new ImageModel();
                                imageModel.IteamId = Data.IteamId;
                                imageModel.ImageData = ms.ToArray();
                                await _image.AddAsync(imageModel);
                            }
                            else
                                ViewBag.file = "png or jpn file format required";

                        }
                        catch
                        {
                            return RedirectToAction("Error", "Error");
                        }
                    }
                    

                }
                string Id = Data.IteamId.ToString();
                return RedirectToAction( "Item", "Manage", new { Id = Id });
            }
            return RedirectToAction("Error", "Error");

        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id, string id)
        {

            if (Id != null|| id != null)
            {
                Guid idIteamDetailId = new Guid(Id);
                Guid idIteamId = new Guid(id);
                var Data = await _iteam.GetByIdAsync(idIteamId);
                var Check = await _iteamDetail.GetById(idIteamDetailId);
                if (Data==null)
                {
                   Data = await _iteam.GetByIdAsync(Check.IteamId);
                }
               
                if (Data != null || Check != null)
                {
                    IteamDetailUpdateData data = new IteamDetailUpdateData();
                    data.images = await _image.GetListByItemIdsync(Data.IteamId);
                    data.item = Data;
                    data.IteamDetailId = Check.IteamDetailId;
                    data.NoOfIteam = Check.NoOfIteam;
                    data.SizeId = Check.SizeId;
                    data.ColorId = Check.ColorId;
                    data.iteamDetailModels = await _iteamDetail.GetListByItemIdsync(Data.IteamId);
                  await  ProductDetailList();
                    await ProductList();
                    ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
                    return View(data);
                }
                else
                    return RedirectToAction("Error", "Error");
            }
            else
                return RedirectToAction("Error", "Error");
        }

        [HttpGet]
        [Route("Manage/Item/{Id}")]
        public async Task<IActionResult> Item(string Id)
        {

            if (Id != null)
            {
                Guid id = new Guid(Id);
                var Data = await _iteam.GetByIdAsync(id);
                if (Data != null)
                {
                    IteamViewData data = new IteamViewData();
                    //ViewBag.ItermName
                    //     ViewBag.ItermName
                    //     ViewBag.Price
                  
                    data.images = await _image.GetListByItemIdsync(Data.IteamId);
                    data.item = Data;
                    data.iteamDetailModels= await _iteamDetail.GetListByItemIdsync(Data.IteamId);
                    await ProductDetailList();
                    await ProductList();
                    ViewBag.CategoryId = new SelectList(await _categoryResp.TabAsync(), "CategoryId", "CategoryName");
                    if(TempData["Data"]!=null)
                    {
                        ViewBag.Data = TempData["Data"].ToString();
                    }
                    return View(data);
                }
                else
                    return RedirectToAction("Error", "Error");
            }
            else
                return RedirectToAction("Error", "Error");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateItem(IteamUpadtemodel Data)
        {

            if (ModelState.IsValid)
            {
       
                    IteamModel Model = new IteamModel();
                    Model.DiscountPrice = Data.DiscountPrice;

                    Model.CategoryId = Data.CategoryId;
                    Model.GanderId = Data.GanderId;
                    Model.ItermName = Data.ItermName;
                    Model.Price = Data.Price;
                   Model.IteamId = Data.Id;
                string Id = Data.Id.ToString();

                await _iteam.UpdatAsync(Model);
             
                //UpdatePrice

                return RedirectToAction( "Item", "Manage", new { Id=Id});
            }
            await ProductList();
            return View(Data);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateItem(string Id)
        {

            if (Id != null)
            {
                Guid id = new Guid(Id);
                var Data = await _iteam.GetByIdAsync(id);
                if (Data != null)
                {
                    IteamUpadtemodel Model = new IteamUpadtemodel();
                    Model.DiscountPrice = Data.DiscountPrice;

                    Model.CategoryId = Data.CategoryId;
                    Model.GanderId = Data.GanderId;
                    Model.ItermName = Data.ItermName;
                    Model.Price = Data.Price;
                    Model.Id = Data.IteamId;
                    await ProductList();
                    return View(Model);
                }
                else
                    return RedirectToAction("Error", "Error");
            }
            else
                return RedirectToAction("Error", "Error");
        }

        //[HttpGet]
        //public async Task<IActionResult> RomoveItem(string IteamId)
        //{

        //    if (IteamId != null)
        //    {
        //        Guid id = new Guid(IteamId);
        //        var Data = await _iteam.GetByIdAsync(id);
        //        if (Data != null)
        //        {
        //            await _iteam.RemoveAsync(id);
        //            return RedirectToAction("List", "Manage");
        //        }
        //        else
        //            return RedirectToAction("NotFound", "Error");
        //    }
        //    else
        //        return RedirectToAction("NotFound", "Error");
        //}
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {

            if (Id != null)
            {
                Guid id = new Guid(Id);
                var Data = await _iteam.GetByIdAsync(id);
                if (Data != null)
                {
                    bool it = false;
                    var uo = await _iteamDetail.GetListByItemIdsync(id);
                    foreach(var dara in uo)
                    {
                      bool au=await RomeIT(dara.IteamDetailId);
                        if (au == false)
                        {
                            it = true;
                            TempData["Data"] = "You can't delete product that is link to an oder";
                            break;
                        }
                    }
                    if(it==false)
                    {
                        await _iteam.RemoveAsync(id);
                    }                  
                    return RedirectToAction("List", "Manage");
                }
                else
                    return RedirectToAction("Error", "Error");
            }
            else
                return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Romove(string Id)
        {

            if (Id != null)
            {
                Guid id = new Guid(Id);
                var Data = await _iteamDetail.GetById(id);
                if (Data != null)
                {
                    bool rr =await RomeIT(Data.IteamDetailId);
                    if (rr==true)
                    {
                        await _iteamDetail.Remove(id);
                    }
                    else
                    {
                        TempData["Data"] = "You can't delete item , that is link to an Oder";
                    }
                    string IteamId = Data.IteamId.ToString();
                    return RedirectToAction( "Item", "Manage", new { Id= IteamId });
                }
                else
                    return RedirectToAction("Error", "Error");
            }
            else
                return RedirectToAction("Error", "Error");
        }       
        public async Task<bool> RomeIT(Guid Id)
        {
            bool Re = true;
            IEnumerable<QuotationDetailModel> de = await  _qoutationDetailz.TabbyItemdetilsAsync(Id);
            if (de == null)
            {
                return Re;
            }
            else
            {
                foreach(var data in de)
                {
                  var Bp=  await _qoutation.GetByIdAsync(data.QoutationId);
                    Guid statuseC =new Guid("6C489A00-BCC8-4DBF-6A00-08D9760E73BA");
                    Guid statuseD = new Guid("4A21A0A9-E908-447F-6A01-08D9760E73BA");
                    if (Bp.QoutationStatuseId!= statuseC|| Bp.QoutationStatuseId != statuseD)
                    {
                        Re = false;
                        break;
                    }
                   
                }
            }
            return Re;
        }


    }
}
