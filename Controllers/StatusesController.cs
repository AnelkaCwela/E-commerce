using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Controllers
{
    [Authorize(Roles = "210fbe4c-3735-4f22-ad9f-06130-45a189c4a266840-d092-4978-b389-1e56e623f2d6")]
    public class StatusesController : Controller
    {
        private readonly ICategory _category;
            private readonly IColor _color;
        private readonly IType _type;
        private readonly IGander _gander;
        private readonly ISize _size;
      private readonly IServiceCategory _serviceCategory;
        private readonly IServiceReserveStatuse _serviceReserveStatuse;
        private readonly IServiceType _serviceType;
        private readonly ISeviceStause _seviceStause;
        private readonly IQoutationStatuse _qoutationStatuse;
        private readonly IStatus _status;
        private readonly ICity _city;
        private readonly IPaymentType _paymentType;
       private readonly IQoutationBySuplier _qoutationBySuplier;
        private readonly IDeliveryStatuse _deliveryStatuse;
        private readonly IIteamStatuse _iteamStatuse;
        private readonly IOderType _oderType;
        private readonly ISupplierStatuse _supplierStatuse;
        private readonly ISupplierType _supplierType;
        private readonly ISubscribeStatuse _subscribeStatuse;
        public StatusesController(ISubscribeStatuse subscribeStatuse,IPaymentType paymentType,IQoutationBySuplier qoutationBySuplier,ISupplierType supplierType,ISupplierStatuse supplierStatuse,ISize size, IGander gander, IColor color, ICategory _ategoryResp, IType Typep,IOderType oderType, IStatus status, IQoutationStatuse qoutationStatuse, ICity city, IDeliveryStatuse deliveryStatuse, IIteamStatuse iteamStatuse, IServiceCategory serviceCategory, ISeviceStause seviceStause, IServiceReserveStatuse serviceReserveStatuse, IServiceType serviceType)
        {
            _subscribeStatuse = subscribeStatuse;
            _paymentType = paymentType;
            _qoutationBySuplier = qoutationBySuplier;
            _supplierStatuse = supplierStatuse;
            _supplierType = supplierType;
            _size = size;
            _gander = gander;
            _color = color;
            _category = _ategoryResp;
            _type = Typep;

            _serviceCategory = serviceCategory;
            _serviceReserveStatuse = serviceReserveStatuse;
            _serviceType = serviceType;
            _seviceStause = seviceStause;
            _deliveryStatuse = deliveryStatuse;
            _status = status;
            _qoutationStatuse = qoutationStatuse;
        
            _city = city;
          
            _oderType = oderType;
            _iteamStatuse = iteamStatuse;
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateSubscribeStatuse(SubscribeStatuseModel model)
        {
            if (ModelState.IsValid)
            {
                await _subscribeStatuse.AddAsync(model);
                ViewBag.Success = "Added Succesfully";
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateSubscribeStatuse()
        {        
            return View();
        }
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> CreateSubscribtionType(QoutationBySuplierModel model)//New , Viewed ,Closed
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _qoutationBySuplier.AddAsync(model);
        //        ViewBag.Success = "Added Succesfully";
        //    }
        //    return View();
        //}
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateqoutationBySuplier(QoutationBySuplierModel model)//New , Viewed ,Closed
        {
            if(ModelState.IsValid)
            {
               await _qoutationBySuplier.AddAsync(model);
                ViewBag.Success = "Added Succesfully";
            }
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreatePaymentType(PaymentTypeModel model)//Bank Deposite ,Online
        {
            if (ModelState.IsValid)
            {
              await  _paymentType.Add(model);
                ViewBag.Success = "Added Succesfully";
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateqoutationBySuplier()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreatePaymentType()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateSupplierType()
        {
           
            return View();
        }
        [HttpGet]
        public IActionResult CreateSupplierStatuse()
        {
           
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateSupplierType(SupplierTypeModel sizeModel)///
        {
            if (ModelState.IsValid)
            {
                await _supplierType.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateSupplierStatuse(SupplierStatuseModel sizeModel)///
        {
            if (ModelState.IsValid)
            {
                await _supplierStatuse.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateSize(SizeModel sizeModel)
        {
            if (ModelState.IsValid)
            {
               await _size.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateGander(GanderModel ganderModel)//
        {
            if (ModelState.IsValid)
            {
               await _gander.AddAsync(ganderModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateColor(ColorModel colorModel)
        {
            if (ModelState.IsValid)
            {
              await  _color.AddAsync(colorModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryModel categoryModel)//ty
        {
            if (ModelState.IsValid)
            {
               await _category.AddAsync(categoryModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateType(TypeModel typeModel)//
        {
            if (ModelState.IsValid)
            {
             await   _type.AddAsync(typeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateSize()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateGander()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateColor()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            ViewBag.TypeId = new SelectList(await _type.TabAsync(), "TypeId", "TypeName");
            return View();
        }
        [HttpGet]
        public IActionResult CreateType()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateDeliveryStatuse(DeliveryStatuseModel sizeModel)
        {
            if (ModelState.IsValid)
            {
              await _deliveryStatuse.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateStatus(StatusModel sizeModel)
        {
            if (ModelState.IsValid)
            {
               await _status.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateQoutationStatuse(QoutationStatuseModel sizeModel)//Submitted ,Pending ,Payment Over due ,Cancelled /Delivered
        {
            if (ModelState.IsValid)
            {
               await _qoutationStatuse.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpGet]
    
        public IActionResult CreateCity()
        {

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateCity(CityModel sizeModel)
        {
            if (ModelState.IsValid)
            {
              await  _city.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateIteamStatuse(IteamStatuseModel sizeModel)//
        {
            if (ModelState.IsValid)
            {
              await  _iteamStatuse.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateOderType(OderTypeModel sizeModel)//Multpli ,Single
        {
            if (ModelState.IsValid)
            {
               await _oderType.AddAsync(sizeModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(sizeModel);
        }
        
       
        [HttpGet]
        public IActionResult CreateDeliveryStatuse()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateStatus()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateQoutationStatuse()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult CreateIteamStatuse()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateOderType(
            )
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateServiceType(ServiceTypeModel seviceStauseModel)
        {
            if (ModelState.IsValid)
            {
              await  _serviceType.AddAsync(seviceStauseModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(seviceStauseModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateServiceReserveStatuse(ServiceReserveStatuseModel seviceStauseModel)
        {
            if (ModelState.IsValid)
            {
              await  _serviceReserveStatuse.AddAsync(seviceStauseModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(seviceStauseModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateSeviceStause(SeviceStauseModel seviceStauseModel)
        {
            if (ModelState.IsValid)
            {
              await  _seviceStause.AddAsync(seviceStauseModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(seviceStauseModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateServiceCategory(ServiceCategoryModel serviceCategoryModel)
        {
            if (ModelState.IsValid)
            {
              await  _serviceCategory.AddAsync(serviceCategoryModel);
                ViewBag.Success = "Added Succesfully";
            }
            else
            {
                ViewBag.USuccess = "Added UnSuccesfully";
            }
            return View(serviceCategoryModel);
        }

        [HttpGet]
        public IActionResult CreateServiceReserveStatuse()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateServiceType()
        {
            ViewBag.ServiceCategoryId = new SelectList(await _serviceCategory.TabAsync(), "ServiceCategoryId", "ServiceCategory");
            return View();
        }
        [HttpGet]
        public IActionResult CreateSeviceStause()
        {

            return View();
        }
        [HttpGet]
        public IActionResult CreateServiceCategory()
        {

            return View();
        }
        

    }
}
