using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OurShop.Controllers
{
      [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
    public class EngController : Controller
    {
        private readonly IOnlinePayMent _onlinePayMent;
        private readonly ICashOnDe _cashOnDe;
        private readonly IBankType _bankType;
        private readonly IDirectbank _directbank;
        private readonly ISupplier _supplier;
        private readonly IPayment _payment;
        private readonly IUserAccount _userAccount;
        private readonly ILogger<EngController> _logger;
        public EngController(ILogger<EngController> logger, IUserAccount userAccount,ISupplier supplier,IDirectbank directbank, IBankType bankType, ICashOnDe cashOnDe, IOnlinePayMent onlinePayMent, IPayment payment)
        {
            _payment = payment;
            _logger = logger;
            _userAccount = userAccount;
            _directbank = directbank;
            _bankType = bankType;
            _supplier = supplier;
            _cashOnDe = cashOnDe;
            _onlinePayMent = onlinePayMent;
     
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
        public async Task<IActionResult> List()
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    BankViewModel Data = new BankViewModel();
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo != null)
                    {
                        CashOnDeModel cash = await _cashOnDe.GetByPayIdAsync(Coo.PaymentId);
                        DirectbankModel directbank = await _directbank.GetByPayIdAsync(Coo.PaymentId);
                        OnlinePayMent onlinePayMent = await _onlinePayMent.GetByPayIdAsync(Coo.PaymentId);
                        Data.onlinePayMent = onlinePayMent;
                        Data.cashOnDeModel = cash;
                        Data.Directbank = directbank;
                    }

                    return View(Data);
                }
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        [Route("Eng/Update/{dd}")]
        public async Task<IActionResult> Update(string dd)
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid id = new Guid(dd);
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        BankTypeModel data = await _bankType.GetByIdAsync(id);
                        return View(data);
                    }

                }
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string U)
        {
            //RemoveAsync
            Anelka();


                if (User.Identity.Name != null)
                {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                    {
                        Guid Id = new Guid(U);
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            await _bankType.RemoveAsync(Id);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(BankTypeModel cashOnDeModel)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            await _bankType.Update(cashOnDeModel);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(cashOnDeModel);
        }
        [HttpGet]
        public async Task<IActionResult> Bank()
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo != null)
                    {
                        BankTypeModel model = new BankTypeModel();
                        var Cooo = await _directbank.GetByPayIdAsync(Coo.PaymentId);

                        if (Cooo != null)
                        {
                            model.DirectbankId = Cooo.DirectbankId;
                        }
                        else
                        {
                            return RedirectToAction("BankPayment", "Eng");
                        }
                        return View(model);
                    }
                   
                }
            }

            return RedirectToAction("Error", "Error");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Bank(BankTypeModel model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        var CooO= await _directbank.GetByIdAsync(Coo.SupplierId);
                       
                        if (Coo != null|| CooO!=null)
                        {
                           
                            await _bankType.AddAsync(model);
                           
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> BankPayment(DirectbankModel model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo != null)
                        {
                            model.PaymentId = Coo.PaymentId;
                            await _directbank.AddAsync(model);
                        }
                        else
                        {
                            PaymentModel mode = new PaymentModel();
                            mode.SupplierId = Co.SupplierId;
                            var pay = await _payment.AddAsync(mode);

                            model.PaymentId = pay.PaymentId;
                            await _directbank.AddAsync(model);
                        }
                        return RedirectToAction("Bank", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> BankPayment()
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo != null)
                    {
                        var Cooo = await _directbank.GetByPayIdAsync(Coo.PaymentId);

                        if (Cooo != null)
                        {
                            string Id = Cooo.DirectbankId.ToString();
                            return RedirectToAction("BankUp", "Eng", new { B = Id });
                        }
                    }
                    return View();
                }
            }

            return RedirectToAction("Error", "Error");
        }
        [HttpPost]
        public async Task<IActionResult> Card()
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                  
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Cooo = await _directbank.GetByPayIdAsync(Coo.PaymentId);

                        if (Cooo == null)
                        {
                            return RedirectToAction("Bank", "Emg");
                        }

                        IEnumerable<BankTypeModel> data = await _bankType.TabByDirectIdAsync(Cooo.DirectbankId);
                        if(data==null)
                        {
                            return RedirectToAction("Bank", "Eng");
                        }
                        return View(data);
                    }

                }
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        [Route("Eng/BankUp/{B}")]
        public async Task<IActionResult> BankUp(string B)
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                 var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid id = new Guid(B);
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        DirectbankModel data = await _directbank.GetByIdAsync(id);
                        return View(data);
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
        public async Task<IActionResult> BankUp(DirectbankModel cashOnDeModel)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            await _directbank.UpdaDirect(cashOnDeModel);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(cashOnDeModel);
        }
        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> OnlinePayment(OnlinePayMent onlinePayMent)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo =await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if(Coo!=null)
                        {
                            onlinePayMent.PaymentId = Coo.PaymentId;
                            await _onlinePayMent.AddAsync(onlinePayMent);
                        }
                        else
                        {
                            PaymentModel model = new PaymentModel();
                            model.SupplierId = Co.SupplierId;
                           var pay =await _payment.AddAsync(model);

                            onlinePayMent.PaymentId = pay.PaymentId;
                            await _onlinePayMent.AddAsync(onlinePayMent);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error","Error");
                }
            }
            return View(onlinePayMent);
        }

        [HttpGet]
        public async Task<IActionResult> CashPayment()
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo != null)
                    {
                        var Cooo = await _cashOnDe.GetByPayIdAsync(Coo.PaymentId);

                        if (Cooo != null)
                        {
                            string Id = Cooo.CashOnDeId.ToString();
                            return RedirectToAction("CashUp", "Eng", new { C = Id });
                        }
                    }
                    return View();
                }
            }

            return RedirectToAction("Error", "Error");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CashUp(CashOnDeModel cashOnDeModel)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            await _cashOnDe.UpdatAsync(cashOnDeModel);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(cashOnDeModel);
        }
        [HttpGet]
        [Route("Eng/CashUp/{C}")]
        public async Task<IActionResult> CashUp(string C)
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    Guid id = new Guid(C);
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        CashOnDeModel data = await _cashOnDe.GetByIdAsync(id);
                        return View(data);
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
        public async Task<IActionResult> CashPayment(CashOnDeModel model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo != null)
                        {
                            model.PaymentId = Coo.PaymentId;
                            await _cashOnDe.AddAsync(model);
                        }
                        else
                        {
                            PaymentModel mode = new PaymentModel();
                            mode.SupplierId = Co.SupplierId;
                            var pay = await _payment.AddAsync(mode);

                            model.PaymentId = pay.PaymentId;
                            await _cashOnDe.AddAsync(model);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(model);
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> OnlinePayment()
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                    var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                    if (Coo != null)
                    {
                    var Cooo =await _onlinePayMent.GetByPayIdAsync(Coo.PaymentId);

                        if(Cooo!=null)
                        {
                            string Id = Cooo.OnlinePayId.ToString();
                            return   RedirectToAction("OnlineUp", "Eng",new { O=Id});
                        }
                    }
                    return View();
                }
            }
               
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        [Route("Eng/OnlineUp/{O}")]
        [Authorize(Roles = "Pro Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> OnlineUp(string O)
        {
            Anelka();
                if (User.Identity.Name != null)
                {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                    {
                         Guid id = new Guid(O);
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo == null)
                        {
                        return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                          OnlinePayMent  data=await _onlinePayMent.GetByIdAsync(id);
                          return View(data);
                         }
                
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
        }
        [HttpPost]
        [Authorize(Roles = "Pro Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> OnlineUp(OnlinePayMent model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var Co = await Product(User.Identity.Name);
                    if (Co == null)
                    {
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        var Coo = await _payment.GetBySupplieIdAsync(Co.SupplierId);
                        if (Coo == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        else
                        {
                            await _onlinePayMent.UpdatAsync(model);
                        }
                        return RedirectToAction("List", "Eng");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(model);
        }
        
       
    }
}
