using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using OurShop.Models.Databinding;
using OurShop.Models;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using Microsoft.Extensions.Logging;
namespace Thumeka.Controllers
{
    [Authorize(Roles = "210fbe4c-3735-4f22-ad9f-06130-45a189c4a266840-d092-4978-b389-1e56e623f2d6")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly UserManager<UserPlusModel> _UserManger;
        private readonly SignInManager<UserPlusModel> _signInManager;
        private readonly ILogger<AdminController> _logger;
        private readonly ISubscribe _subscribe;
        private readonly ISubscribeStatuse _subscribeStatuse;
        private readonly ISubscribtionType _subscribtionType;
        private readonly IAdmin _admin;
        private readonly IPay _Pay;
        public readonly ISupplier _supplier;
        private readonly IBrand _brand;
        private readonly IContactDetail _detailContact;
        private readonly IResident _resident;
        private readonly IBrandDeatail _brandDeatail;
        private readonly ISupplierStatuse _supplierStatuse;
        private readonly ISupplierType _supplierType;
       private readonly ICity _city;
        private readonly ILoginTrack _loginTrack;

        public AdminController(ILoginTrack loginTrack,ILogger<AdminController> logger, ISubscribeStatuse subscribeStatuse, ISubscribe subscribe, ISupplierType supplierType, ISupplierStatuse supplierStatuse, ICity city,  IBrand brand, IContactDetail detail, IResident resident, IBrandDeatail brandDeatail, ISupplier supplier,ISubscribtionType subscribtionType,IPay Pay,IAdmin admin,RoleManager<IdentityRole> RoleManger, UserManager<UserPlusModel> UserManger, SignInManager<UserPlusModel> signInManager)
        {
            _subscribeStatuse = subscribeStatuse;
            _loginTrack = loginTrack;
            _subscribe = subscribe;
            _supplierType = supplierType;
            _admin = admin;
            _supplierStatuse = supplierStatuse;
            _supplier = supplier;
            _Pay = Pay;
            _city = city;
            _brand = brand;
            _detailContact = detail;
            _resident = resident;
            _brandDeatail = brandDeatail;
            _subscribtionType = subscribtionType;
            this._RoleManger = RoleManger;
            this._UserManger = UserManger;
            this._signInManager = signInManager;
           
        }

        private static readonly string Identity = "wwwroot/Document/Identity.pdf";
        private static readonly string Residance = "wwwroot/Document/Residance.pdf";
        private static readonly string LOGO = "wwwroot/Images/logo-sm.png";
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

        public async Task<IActionResult> IdentDoc(string Id)
        {
            Anelka();
            if (Id == null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                Guid id = new Guid(Id);
                var supp=  await _supplier.GetByIdAsync(id);
                if (supp==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                     var brand=await _brandDeatail.GetByIdAsync(supp.BrandDeatailId);
                    if (brand==null)
                    {                        
                            return RedirectToAction("Error", "Error");                        
                    }
                    else
                    {
                        byte[] file = brand.IdentityDocument;
                        return File(file, "application/pdf");
                    }
                }
               
            }

        }
        [HttpGet]
        public async Task<IActionResult> ResDoc(string Id)
        {
            Anelka();
            if (Id == null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                Guid id = new Guid(Id);
                var supp = await _supplier.GetByIdAsync(id);
                if (supp == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var brand = await _brandDeatail.GetByIdAsync(supp.BrandDeatailId);
                    if (brand == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        byte[] file = brand.IdentityDocument;
                        return File(file, "application/pdf");
                    }
                }

            }

        }

        [HttpGet]
        public async Task<IActionResult> Standard(string id)
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (Co == null||id==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid SupplierId = new Guid(id);
                    var su = await _supplier.GetByIdAsync(SupplierId);
                    if(su==null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var user = await _UserManger.FindByEmailAsync(su.SupplierUserName);
                        AdminSubscribe model = new AdminSubscribe();
                        model.Id = SupplierId;
                        model.Ln = user.Name;
                        model.Ln = user.Surname;
                        ViewBag.Id = new SelectList(await _subscribtionType.TabAsync(), "SubscribtionTypeId", "SubscribtionType");
                        return View(model);
                    }
                }
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Standard(AdminSubscribe mode)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await _admin.GetByUserNameAsync(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        Guid Statuseid = new Guid("C80874A1-005B-4658-E9A6-08D97ADC4CFF");
                        Guid SubTypeid = new Guid("2C15603D-0D14-43FD-28B6-08D963E780C5");
                        var Data = await _subscribtionType.GetByIdAsync(mode.SubId);
                        if (Data != null)
                        {
                            SubscribeModel model = new SubscribeModel();
                            model.AmountPayed = Data.Price;
                            model.DateSubscribe = DateTime.Now;
                            model.SubscribeStatuseId = Statuseid;
                            model.SubscribtionTypeId = mode.SubId;
                            model.SupplierId = mode.Id;
                            model.Statuse = true;
                            model.EndDate = DateTime.Now.AddDays(Data.NoOfdays);
                               await _subscribe.AddAsync(model);
                            var supp = await _supplier.GetByIdAsync(mode.Id);
                            if(supp==null)
                            {
                                return RedirectToAction("Error", "Error");
                            }
                            var userName = await _UserManger.FindByEmailAsync(supp.SupplierUserName);
                            if (userName == null)
                            {
                                return RedirectToAction("Error", "Error");
                            }
                            supp.SupplierTypeId = SubTypeid;
                            string Role = "Stand Plan";
                            await _UserManger.AddToRoleAsync(userName, Role);
                            await _supplier.UpdatAsync(supp);

                        }
                        else
                        {
                            return RedirectToAction("Error", "Error");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            ViewBag.Id = new SelectList(await _subscribtionType.TabAsync(), "SubscribtionTypeId", "SubscribtionType");
            return View(mode);
        }
        [HttpGet]
        public async Task<IActionResult> Block(string Id)
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);

                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                            Guid s = new Guid(Id);                     
                            var updt = await _subscribe.GetByIdAsync(s);
                            if (updt != null)
                            {
                                Guid id = new Guid("E467FAB6-FEA4-40F0-E9A7-08D97ADC4CFF");
                                updt.Statuse = false;
                                updt.SubscribeStatuseId = id;
                                await _subscribe.UpdaAsync(updt);
                            }
                            
                            return RedirectToAction("FreePlan", "Admin");
                }

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Remove(string Ip)
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);

                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid s = new Guid(Ip);
                    var supplier = await _supplier.GetByIdAsync(s);
                    if (supplier == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var userName = await _UserManger.FindByEmailAsync(supplier.SupplierUserName);
                        if (userName == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            Guid suscribedStatuseId = new Guid("C80874A1-005B-4658-E9A6-08D97ADC4CFF");
                           var updt=await _subscribe.GetBySupplierStatuseIdAsync(supplier.SupplierId, suscribedStatuseId);
                            if(updt!=null)
                            {
                                Guid id = new Guid("E467FAB6-FEA4-40F0-E9A7-08D97ADC4CFF");
                                updt.Statuse = false;
                                updt.SubscribeStatuseId = id;
                              await  _subscribe.UpdaAsync(updt);
                            }
                            Guid supplierId = new Guid("5395D734-3CDC-4C3B-28B7-08D963E780C5");
                            string Remove = "Stand Plan";
                            supplier.SupplierTypeId = supplierId;
                            await _UserManger.RemoveFromRoleAsync(userName, Remove);
                            await _supplier.UpdatAsync(supplier);
                            return RedirectToAction("FreePlan", "Admin");
                        }
                    }
                }

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Approve(string Id)
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);

                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid s = new Guid(Id);
                    var supplier = await _supplier.GetByIdAsync(s);
                    if(supplier==null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var userName = await _UserManger.FindByEmailAsync(supplier.SupplierUserName);
                        if(userName==null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            Guid supplierId = new Guid("EA45280F-8811-4D04-559B-08D963E7ACB0");
                            string Role = "Free Plan";
                            string Remove = "Sell";
                            supplier.SupplierStatuseId = supplierId;
                            await _UserManger.AddToRoleAsync(userName, Role);
                            await _UserManger.RemoveFromRoleAsync(userName, Remove);
                            await _supplier.UpdatAsync(supplier);
                            return RedirectToAction("FreePlan", "Admin");
                        }
                    }
                }
             
            }
            return RedirectToAction("Error", "Error");
        }
        public async Task<List<SupplierModel>> SubRole(string Role)
        {
            IEnumerable<UserPlusModel> model = await _UserManger.GetUsersInRoleAsync(Role);
            IEnumerable<SupplierModel> sup = await _supplier.TabAsync();
            List<SupplierModel> Data = new List<SupplierModel>();
            foreach (var mode in model)
            {
                foreach(var su in sup)
                {
                    if(su.SupplierUserName== mode.Email)
                    {
                        Data.Add(su);
                    }
                }
            }
            return Data;
        }
        
        [HttpGet]
        public async Task<IActionResult> lOG()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {

                    IEnumerable<LoginTrack> suppliers = await  _loginTrack.TabAsync();


                    return View(suppliers);
                }

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Verify()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    string Role = "Sell";

                    IEnumerable<SupplierModel> suppliers = await SubRole(Role);
                    IEnumerable<BrandModel> brands = await _brand.TabAsync();
                    IEnumerable<BrandDeatailModel> brandDeatails = await _brandDeatail.TabAsync();
                    IEnumerable<SupplierStatuseModel> supplierStatuses = await _supplierStatuse.TabAsync();
                    IEnumerable<SupplierTypeModel> supplierTypes = await _supplierType.TabAsync();
                    var combineTible = from c in suppliers
                                       join ct in brands on c.BrandId equals ct.BrandId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in brandDeatails on c.BrandDeatailId equals xn24.BrandDeatailId into tab24
                                       from xn24 in tab24.DefaultIfEmpty()

                                       join xn99 in supplierStatuses on c.SupplierStatuseId equals xn99.SupplierStatuseId into tab99
                                       from xn99 in tab99.DefaultIfEmpty()

                                       join x in supplierTypes on c.SupplierTypeId equals x.SupplierTypeId into x
                                       from xq in x.DefaultIfEmpty()

                                       select new AdminSupplierViewModel
                                       {
                                           SupplierModel = c,
                                           SupplierTypeModel = xq,
                                           BrandModel = ct,
                                           SupplierStatuseModel = xn99
                                           ,
                                           BrandDeatailModel = xn24
                                       };

                    return View(combineTible);
                }

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> FreePlan()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    string Role = "Free Plan";

                    IEnumerable<SupplierModel> suppliers = await SubRole(Role);
                    IEnumerable<BrandModel> brands = await _brand.TabAsync();
                    IEnumerable<BrandDeatailModel> brandDeatails = await _brandDeatail.TabAsync();
                    IEnumerable<SupplierStatuseModel> supplierStatuses = await _supplierStatuse.TabAsync();
                    IEnumerable<SupplierTypeModel> supplierTypes = await _supplierType.TabAsync();
                    var combineTible = from c in suppliers
                                       join ct in brands on c.BrandId equals ct.BrandId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in brandDeatails on c.BrandDeatailId equals xn24.BrandDeatailId into tab24
                                       from xn24 in tab24.DefaultIfEmpty()

                                       join xn99 in supplierStatuses on c.SupplierStatuseId equals xn99.SupplierStatuseId into tab99
                                       from xn99 in tab99.DefaultIfEmpty()

                                       join x in supplierTypes on c.SupplierTypeId equals x.SupplierTypeId into x
                                       from xq in x.DefaultIfEmpty()

                                       select new AdminSupplierViewModel
                                       {
                                           SupplierModel = c,
                                           SupplierTypeModel = xq,
                                           BrandModel = ct,
                                           SupplierStatuseModel = xn99
                                           ,
                                           BrandDeatailModel = xn24
                                       };

                    return View(combineTible);
                }

            }
            return RedirectToAction("Error", "Error");
        }

        [HttpGet]
        public async Task<IActionResult> StandPlan()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    string Role = "Stand Plan";

                    IEnumerable<SupplierModel> suppliers = await SubRole(Role);
                    IEnumerable<BrandModel> brands = await _brand.TabAsync();
                    IEnumerable<BrandDeatailModel> brandDeatails = await _brandDeatail.TabAsync();
                    IEnumerable<SupplierStatuseModel> supplierStatuses = await _supplierStatuse.TabAsync();
                    IEnumerable<SupplierTypeModel> supplierTypes = await _supplierType.TabAsync();
                    var combineTible = from c in suppliers
                                       join ct in brands on c.BrandId equals ct.BrandId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in brandDeatails on c.BrandDeatailId equals xn24.BrandDeatailId into tab24
                                       from xn24 in tab24.DefaultIfEmpty()

                                       join xn99 in supplierStatuses on c.SupplierStatuseId equals xn99.SupplierStatuseId into tab99
                                       from xn99 in tab99.DefaultIfEmpty()

                                       join x in supplierTypes on c.SupplierTypeId equals x.SupplierTypeId into x
                                       from xq in x.DefaultIfEmpty()

                                       select new AdminSupplierViewModel
                                       {
                                           SupplierModel = c,
                                           SupplierTypeModel = xq,
                                           BrandModel = ct,
                                           SupplierStatuseModel = xn99
                                           ,
                                           BrandDeatailModel = xn24
                                       };

                    return View(combineTible);
                }

            }
            return RedirectToAction("Error", "Error");
        }




        [HttpGet]
        public async Task<IActionResult> Plan(string Ip)
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null|| Ip == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid id = new Guid(Ip);
                    IEnumerable<SupplierModel> suppliers = await _supplier.TabAsync();
                    IEnumerable<SubscribtionTypeModel> subscribtionTypes = await _subscribtionType.TabAsync();
                    IEnumerable<SubscribeStatuseModel> subscribeStatuses = await _subscribeStatuse.TabAsync();
                    IEnumerable<SubscribeModel> subscribes = await _subscribe.TabBySupplierIdAsync(id);
                    var combineTible = from c in subscribes
                                       join ct in suppliers on c.SupplierId equals ct.SupplierId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in subscribtionTypes on c.SubscribtionTypeId equals xn24.SubscribtionTypeId into tab24
                                       from xn24 in tab24.DefaultIfEmpty()

                                       join xn99 in subscribeStatuses on c.SubscribeStatuseId equals xn99.SubscribeStatuseId into tab99
                                       from xn99 in tab99.DefaultIfEmpty()

                                       select new AdminSubribedViewModel
                                       {
                                           subscribeModel = c,
                                           SubscribtionTypeModel = xn24,
                                           SupplierModel = ct,
                                           subscribeStatuseModel = xn99
                                        
                                       };

                    return View(combineTible);
                }
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    IEnumerable<SupplierModel> suppliers = await _supplier.TabAsync();
                    IEnumerable<BrandModel> brands = await _brand.TabAsync();
                    IEnumerable<BrandDeatailModel> brandDeatails = await _brandDeatail.TabAsync();
                    IEnumerable<SupplierStatuseModel> supplierStatuses = await _supplierStatuse.TabAsync();
                    IEnumerable<SupplierTypeModel> supplierTypes = await _supplierType.TabAsync();
                    var combineTible = from c in suppliers
                                       join ct in brands on c.BrandId equals ct.BrandId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in brandDeatails on c.BrandDeatailId equals xn24.BrandDeatailId into tab24
                                       from xn24 in tab24.DefaultIfEmpty()

                                       join xn99 in supplierStatuses on c.SupplierStatuseId equals xn99.SupplierStatuseId into tab99
                                       from xn99 in tab99.DefaultIfEmpty()

                                       join x in supplierTypes on c.SupplierTypeId equals x.SupplierTypeId into x
                                       from xq in x.DefaultIfEmpty()

                                       select new AdminSupplierViewModel
                                       {
                                           SupplierModel = c,
                                           SupplierTypeModel = xq,
                                           BrandModel = ct,
                                           SupplierStatuseModel = xn99
                                           ,
                                           BrandDeatailModel= xn24
                                       };

                    return View(combineTible);
                }

            }
            return RedirectToAction("Error", "Error");
        }
       
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> SubcrptionType(SubscribtionTypeModel model)
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);

                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    model.AdminId = data.AdminId;
                    await _subscribtionType.AddAsync(model);
                }
                return View();
            }
            return RedirectToAction("Error", "Home");
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Pay(PayModel model)
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);

                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    model.AdminId = data.AdminId;
                    await _Pay.AddAsync(model);
                }
                return View();
            }
            return RedirectToAction("Error", "Error");
        }
       
        [HttpGet]
        public async Task<IActionResult> Pay()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                return View();
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> SubcrptionType()
        {
            if (User.Identity.Name != null)
            {
                var data = await _admin.GetByUserNameAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                return View();
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
       
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _RoleManger.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Admin");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }

            return View(model);
        }
       
        [HttpGet]
        public IActionResult ListRole()
        {
            var role = _RoleManger.Roles;

            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await _RoleManger.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {Id} cannet be found";
                return View("NotFound");
            }
            var model = new EditRoleModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            //if(ConnectionState.Open==State)
            //{ 

            //}
            foreach (var user in _UserManger.Users)
            {
                //
                if (await _UserManger.IsInRoleAsync(user, role.Name))
                {
                    //mode.User.Add(use.Name);
                    model.Users.Add(user.UserName);


                }
            }
            return View(model);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            var role = await _RoleManger.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _RoleManger.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string RoleId)
        {
            ViewBag.RoleId = RoleId;
            var role = await _RoleManger.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {RoleId} cannet be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            //List<StaffModel> sample=_usersample != null.
            foreach (var user in _UserManger.Users)
            {
                //StaffModel sample = _user.GetStaffByEmail(user.UserName);
                //if ()
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName

                    };
                    if (await _UserManger.IsInRoleAsync(user, role.Name))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        userRoleViewModel.IsSelected = false;
                    }
                    model.Add(userRoleViewModel);
                }
            }
            return View(model);
        }
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> AddUserToRole(List<UserRoleViewModel> model, string RoleId)
        {
            //RoleId = ViewBag.RoleId;

            var role = await _RoleManger.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {model[0].RoleIdd} cannet be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _UserManger.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _UserManger.IsInRoleAsync(user, role.Name)))
                {
                    result = await _UserManger.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _UserManger.IsInRoleAsync(user, role.Name))
                {
                    result = await _UserManger.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = RoleId });
                }
            }
            return RedirectToAction("EditRole", new { id = RoleId });
        }
  
    }
}
