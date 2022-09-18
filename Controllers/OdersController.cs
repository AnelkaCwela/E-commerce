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

namespace OurShop.Controllers
{
    public class OdersController : Controller
    {

        private readonly IImage _image;
        private readonly IUserAccount _userAccount;
        private readonly IOderType _oderType;
        private readonly IRefrance _refrance;
        private readonly ISize _size;
        private readonly IGander _gander;
        private readonly IColor _color;
        private readonly IPaymentType _paymentType;
        private readonly ICategory _categoryResp;
        private readonly IType _type;
        private readonly IIteam _iteam;
        private readonly ICustomer _customer;
        private readonly ISupplier _supplier;
        private readonly IIteamStatuse _iteamStatuse;
        private readonly IIteamDetail _iteamDetail;
        private readonly IBrand _brand;
        public readonly ICity _city;
        private readonly ISubub _subub;
        private readonly IQoutationDetailz _qoutationDetailz;
        private readonly IQoutation _qoutation;
        private readonly IQoutationStatuse _qoutationStatuse;
        private readonly IQoutationBySuplier _qoutationBySuplier;
        private readonly ILike _like;
        private readonly IRating _rating;
        public OdersController(IUserAccount userAccount,IRating rating, IOderType oderType,ILike like,IQoutationBySuplier qoutationBySuplier, IQoutationStatuse qoutationStatuse,IPaymentType paymentType,IRefrance refrance, IQoutation qoutation, IQoutationDetailz qoutationDetailz, ISubub subub, ICity city, IBrand brand, IIteamDetail iteamDetail, IIteamStatuse iteamStatuse, ISupplier supplier, ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _like = like;
            _rating = rating;
            _userAccount = userAccount;
            _oderType = oderType;
            _qoutationBySuplier = qoutationBySuplier;
            _qoutationStatuse = qoutationStatuse;
            _paymentType = paymentType;
            _refrance = refrance;
            _qoutationDetailz = qoutationDetailz;
            _qoutation = qoutation;
            _subub = subub;
            _city = city;
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
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Item(string Id)
        {
            Guid id = new Guid(Id);
            IEnumerable<QuotationDetailModel> quotationDetails = await _qoutationDetailz.TabbyIdAsync(id);
            return View(quotationDetails);

        }
        [HttpGet]
        public async Task<IActionResult> Ite(string Id)
        {
            Guid id = new Guid(Id);
            IEnumerable<QuotationDetailModel> quotationDetails = await _qoutationDetailz.TabbyIdAsync(id);
            return View(quotationDetails);

        }
        [HttpPost]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Ref(string Refrance)
        {
            var refrance = await _refrance.GetById(Refrance);
            QuotationModel model = new QuotationModel();
            if (refrance != null)
            {
                model = await _qoutation.GetByIdAsync(refrance.QoutationId);
                if (model == null)
                {
                    TempData["Data"] = "The refrance entred do exit or not found , please contact the store";
                    return RedirectToAction("Oders", "Oders");
                }
                else
                {
                    IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
                    IEnumerable<QuotationModel> qo = await _qoutation.TabIdAsync(model.QoutationId);
                    IEnumerable<SububModel> Sub = await _subub.TabAsync();
                    IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
                    IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
                    IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
                    var combineTible = from c in qo
                                       join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                                       from xn24 in tab2.DefaultIfEmpty()
                                       join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                                       from xn00 in tab00.DefaultIfEmpty()

                                       join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                                       from xn99 in tab3.DefaultIfEmpty()

                                       join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                                       from xn9 in tab9.DefaultIfEmpty()
                                       select new OderListView
                                       {
                                           Oder = xn00,
                                           SububModel = ct,
                                           qota = c,
                                           PaymentTypeModel = xn24,
                                           QoutationStatuseModel = xn99,
                                           QoutationBySuplierMod = xn9
                                       };
                    return View(combineTible);
                }
            }
            else
            {
                TempData["Data"] = "The refrance entred do exit or not found , please contact the store";
                return RedirectToAction("Oders", "Oders");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Refrance(string Refrance)
        {
               var refrance = await _refrance.GetById(Refrance);
            QuotationModel model = new QuotationModel();
            if (refrance!=null)
            {
                model = await _qoutation.GetByIdAsync(refrance.QoutationId);
                if(model == null)
                {
                    TempData["Data"] = "The refrance entred do exit or not found , please contact the store";
                    return RedirectToAction("Track", "Oders");
                }
                else
                {
                    IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
                    IEnumerable<QuotationModel> qo = await _qoutation.TabIdAsync(model.QoutationId);
                    IEnumerable<SububModel> Sub = await _subub.TabAsync();
                    IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
                    IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
                    IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
                    var combineTible = from c in qo
                                       join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                                       from ct in tab1.DefaultIfEmpty()

                                       join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                                       from xn24 in tab2.DefaultIfEmpty()
                                       join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                                       from xn00 in tab00.DefaultIfEmpty()

                                       join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                                       from xn99 in tab3.DefaultIfEmpty()

                                       join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                                       from xn9 in tab9.DefaultIfEmpty()
                                       select new OderListView
                                       {
                                           Oder = xn00,
                                           SububModel = ct,
                                           qota = c,
                                           PaymentTypeModel = xn24,
                                           QoutationStatuseModel = xn99,
                                           QoutationBySuplierMod = xn9
                                       };
                    return View(combineTible);
                }
            }
            else
            {
                TempData["Data"] = "The refrance entred do exit or not found , please contact the store";
                return RedirectToAction("Track", "Oders");
            }
  
        }
        [HttpGet]
        public async Task<IActionResult> Em(string Id)
        {
            Guid id = new Guid(Id);
            IEnumerable<QuotationDetailModel> quotationDetails = await _qoutationDetailz.TabbyIdAsync(id);
            return View(quotationDetails);

        }

        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Shipping(string Id)
        {
            Guid id = new Guid(Id);
            SububModel sububModel = await _subub.GetByIdAsync(id);
            ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
            return View(sububModel);
        }
        [HttpGet]
        public async Task<IActionResult> Shipp(string Id)
        {
            Guid id = new Guid(Id);
            SububModel sububModel = await _subub.GetByIdAsync(id);
            ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
            return View(sububModel);
        }
        public async Task<IActionResult> Track()
        {

            if (User.Identity.Name == null)
            {
                return RedirectToAction("Error", "Error");
            }
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
            if(TempData["Data"]!=null)
            {
                ViewBag.Data = TempData["Data"];
            }

            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
            IEnumerable<QuotationModel> qo = await _qoutation.TabCustomerAsync(Cus.CustomerId);
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Oders()
        {

            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            if (TempData["Data"] != null)
            {
                ViewBag.Data = TempData["Data"];
            }
            IEnumerable<QuotationModel> qo = await _qoutation.TabSuppIdAsync(Co.SupplierId);

            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();       
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            //IEnumerable<RefranceModel> Refranc = await _refrance.Tab();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Online()
        {
            Guid statuse = new Guid("A1DF485C-7993-413B-B2DE-08D9760E0E43");//Canceled
            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            IEnumerable<QuotationModel> qo = await _qoutation.TabSuppIdAsync(Co.SupplierId);
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.GetById(statuse);
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Bank()
        {
            Guid statuse = new Guid("14773336-203B-4D08-B2DF-08D9760E0E43");//Canceled
            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            IEnumerable<QuotationModel> qo = await _qoutation.TabSuppIdAsync(Co.SupplierId);
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.GetById(statuse);
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
       [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Canceled()
        {
            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            Guid statuse = new Guid("6C489A00-BCC8-4DBF-6A00-08D9760E73BA");//Canceled
            IEnumerable<QuotationModel> qo = await _qoutation.TabStatuseAsync(statuse, Co.SupplierId);
 
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();

            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        [HttpGet]
        public async Task<IActionResult> Shipped()
        {
            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            Guid statuse = new Guid("4A21A0A9-E908-447F-6A01-08D9760E73BA");//Canceled
            IEnumerable<QuotationModel> qo = await _qoutation.TabStatuseAsync(statuse, Co.SupplierId);
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Payed()
        {
            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            Guid statuse = new Guid("1E68D658-8125-4DB9-69FD-08D9760E73BA");//Submitted/Payed
            IEnumerable<QuotationModel> qo = await _qoutation.TabStatuseAsync(statuse, Co.SupplierId);
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();

            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> OverDue()
        {

            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            Guid statuse = new Guid("486983A8-CBEA-4337-69FF-08D9760E73BA");//OverDue
            IEnumerable<QuotationModel> qo = await _qoutation.TabStatuseAsync(statuse, Co.SupplierId);

            
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Pending()
        {

            var Co = await Produc(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            Guid statuse = new Guid("9DA0B3D4-1671-48F3-69FE-08D9760E73BA");//Padding
            IEnumerable<QuotationModel> qo = await _qoutation.TabStatuseAsync(statuse, Co.SupplierId);
           
            IEnumerable<OderTypeModel> type = await _oderType.TabAsync();
           
            IEnumerable<SububModel> Sub = await _subub.TabAsync();
            IEnumerable<PaymentTypeModel> pay = await _paymentType.Tab();
            IEnumerable<QoutationStatuseModel> Sta = await _qoutationStatuse.TabAsync();
            IEnumerable<QoutationBySuplierModel> QoBySup = await _qoutationBySuplier.TabAsync();
            var combineTible = from c in qo
                               join ct in Sub on c.SubrbId equals ct.SubrbId into tab1
                               from ct in tab1.DefaultIfEmpty()

                               join xn24 in pay on c.PaymentTypeId equals xn24.PaymentTypeId into tab2
                               from xn24 in tab2.DefaultIfEmpty()
                               join xn00 in type on c.OderTypeId equals xn00.OderTypeId into tab00
                               from xn00 in tab00.DefaultIfEmpty()

                               join xn99 in Sta on c.QoutationStatuseId equals xn99.QoutationStatuseId into tab3
                               from xn99 in tab3.DefaultIfEmpty()

                               join xn9 in QoBySup on c.QoutationStatuseSuplierId equals xn9.QoutationStatuseSuplierId into tab9
                               from xn9 in tab9.DefaultIfEmpty()
                               select new OderListView
                               {
                                   Oder = xn00,
                                   SububModel = ct,
                                   qota = c,
                                   PaymentTypeModel = xn24,
                                   QoutationStatuseModel = xn99,
                                   QoutationBySuplierMod = xn9
                               };
            return View(combineTible);
        }
        public async Task<SupplierModel> Produc(string UserName)
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
        private async Task ProductDetailList()
        {
            ViewBag.SizeId = new SelectList(await _size.TabAsync(), "SizeId", "Size");
            ViewBag.ColorId = new SelectList(await _color.TabAsync(), "ColorId", "Color");
        }

        //
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Update(string Id)
        {
            Guid id = new Guid(Id);
            QuotationModel Data = await _qoutation.GetByIdAsync(id);
            UpdateQoutationStatuse model = new UpdateQoutationStatuse();
            model.PaymentTypeId = Data.PaymentTypeId;
            model.QoutationDate = Data.QoutationDate;
            model.QoutationStatuseId = Data.QoutationStatuseId;
            model.TotalQoutationPrice = Data.TotalQoutationPrice;
            model.QoutationId = Data.QoutationId;
            ViewBag.PaymentTypeId = new SelectList(await _paymentType.Tab(), "PaymentTypeId", "PaymentType");
            ViewBag.QoutationStatuseId = new SelectList(await _qoutationStatuse.TabAsync(), "QoutationStatuseId", "QoutationStatuse");
            return View(model);
        }
         [HttpPost]
         [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Update(UpdateQoutationStatuse Data)
        {
           if(ModelState.IsValid)
            {
                QuotationModel model = new QuotationModel();
                model.PaymentTypeId = Data.PaymentTypeId;
                model.QoutationDate = Data.QoutationDate;
                model.QoutationStatuseId = Data.QoutationStatuseId;
                model.TotalQoutationPrice = Data.TotalQoutationPrice;
                model.QoutationId = Data.QoutationId;
                await _qoutation.UpdaStatuseAsync(model);
                return RedirectToAction("Oders", "Oders");
            }
            return View(Data);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public IActionResult Analytics()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Likes()
        {
            if (User.Identity.Name != null)
            {                
                var Co = await Produc(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                IEnumerable<IteamModel> Item = await _iteam.TabGroupAsync(Co.SupplierId);


                IEnumerable<LikeModel> li = await _like.TabAsync();

                var follow = await _rating.TabSupplierAsync(Co.SupplierId);
                ViewBag.Follow = follow.Count();

                var combineTible = from c in li
                                   join ct in Item on c.IteamId equals ct.IteamId into tab1
                                   from ct in tab1.DefaultIfEmpty()

                                   select new LikeViewModel
                                   {
                                       IteamModel = ct,
                                       LikeModel = c
                                   };
                return View(combineTible);
            }
            return RedirectToAction("Error", "Error");

        }

        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Views()
        {
            if (User.Identity.Name != null)
            {
                var Co = await Produc(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var follow = await _rating.TabSupplierAsync(Co.SupplierId);
                    ViewBag.Follow = follow.Count();
                    IEnumerable<IteamModel> Model = await _iteam.TabGroupAsync(Co.SupplierId);

                    return View(Model.Where(p=>p.View!=0));
                }
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Product(string Id)
        {
            if(Id!=null)
            {
                ItemDView dd = new ItemDView();
                Guid id = new Guid(Id);
                IteamDetailModel model = await _iteamDetail.GetById(id);
                if(model == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                IteamModel mo = await _iteam.GetByIdAsync(model.IteamId);
               await ProductDetailList();
                ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
                dd.ColorId = model.ColorId;
                dd.GanderId = mo.GanderId;
                dd.ImageData = mo.ImageIteam;
                dd.SizeId = model.SizeId;
                dd.ItermName = mo.ItermName;
                return View(dd);

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Produ(string Id)
        {
            if (Id != null)
            {
                ItemDView dd = new ItemDView();
                Guid id = new Guid(Id);
                IteamDetailModel model = await _iteamDetail.GetById(id);
                if (model == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                IteamModel mo = await _iteam.GetByIdAsync(model.IteamId);
                await ProductDetailList();
                ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
                dd.ColorId = model.ColorId;
                dd.GanderId = mo.GanderId;
                dd.ImageData = mo.ImageIteam;
                dd.SizeId = model.SizeId;
                dd.ItermName = mo.ItermName;
                return View(dd);

            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Pro(string Id)
        {
            if (Id != null)
            {
                ItemDView dd = new ItemDView();
                Guid id = new Guid(Id);
                IteamDetailModel model = await _iteamDetail.GetById(id);
                if (model == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                IteamModel mo = await _iteam.GetByIdAsync(model.IteamId);
                await ProductDetailList();
                ViewBag.GanderId = new SelectList(await _gander.TabAsync(), "GanderId", "Gander");
                dd.ColorId = model.ColorId;
                dd.GanderId = mo.GanderId;
                dd.ImageData = mo.ImageIteam;
                dd.SizeId = model.SizeId;
                dd.ItermName = mo.ItermName;
                return View(dd);

            }
            return RedirectToAction("Error", "Error");
        }

    }
}
