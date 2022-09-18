using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayFast;
using PayFast.AspNetCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.AspNetCore.Identity;
using OurShop.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace OurShop.Controllers
{
    [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
    public class PaymentController : Controller
    {
        private readonly IOnlinePayMent _onlinePayMent;
        private readonly ICashOnDe _cashOnDe;
        private readonly IBankType _bankType;
        private readonly IDirectbank _directbank;
        private readonly ISupplier _supplier;
        private readonly IPayment _payment;
        private readonly ISubscribe _subscribe;
        private readonly ISubscribeStatuse _subscribeStatuse;
        private readonly ILogger<PaymentController> _logger;
        private readonly ISubscribtionType _subscribtionType;
        private readonly IPay _pay;
        private readonly SignInManager<UserPlusModel> _signInManager;
        private readonly IUserAccount _userAccount;
        private readonly UserManager<UserPlusModel> _userManager;
        //  private readonly 
        public PaymentController(ILogger<PaymentController> logger, SignInManager<UserPlusModel> signInManager,IUserAccount userAccount,UserManager<UserPlusModel> userManager,IPay pay,ISubscribtionType subscribtionType,ISubscribeStatuse subscribeStatuse,ISubscribe subscribe,ISupplier supplier, IDirectbank directbank, IBankType bankType, ICashOnDe cashOnDe, IOnlinePayMent onlinePayMent, IPayment payment)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _pay = pay;
            _userManager = userManager;
            _userAccount = userAccount;
            _subscribtionType = subscribtionType;
            _subscribeStatuse = subscribeStatuse;
            _subscribe = subscribe;
            _payment = payment;
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
        public async Task<IActionResult> Standard()
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
                    Guid typeid = new Guid("41EBBCA2-32AE-4EB6-001D-08D97ADE085E");
                    Guid Statuseid = new Guid("C81FFDD0-25D8-4F9C-E9A8-08D97ADC4CFF");
                    var Data = await _subscribtionType.GetByIdAsync(typeid);
                    if(Data!=null)
                    {
                        SubscribeModel model = new SubscribeModel();
                        model.AmountPayed =Data.Price;
                        model.DateSubscribe = DateTime.Now;
                        model.SubscribeStatuseId = Statuseid;
                        model.SubscribtionTypeId = typeid;
                        model.SupplierId = Co.SupplierId;
                        model.Statuse = false;
                        model.EndDate = DateTime.Now.AddDays(Data.NoOfdays);
                        var retun = await _subscribe.AddAsync(model);
                        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                        if (user == null || retun == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        string des = "50 Items , Accept Online Payment with (payfast) , 3 - User - Account (R"+ Data.Price + " p/m)";
                        var redirectUrl= await Pay(retun, Co.SupplierUserName, des, user.Name,user.Surname);
                     return Redirect(redirectUrl);
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
        [HttpGet]
        public async Task<IActionResult> StandardT()
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
                    Guid typeid = new Guid("CB1321FE-2241-46B0-9082-08D9A36AA8F5");
                    Guid Statuseid = new Guid("C81FFDD0-25D8-4F9C-E9A8-08D97ADC4CFF");
                    var Data = await _subscribtionType.GetByIdAsync(typeid);
                    if (Data != null)
                    {
                        SubscribeModel model = new SubscribeModel();
                        model.AmountPayed = Data.Price;
                        model.DateSubscribe = DateTime.Now;
                        model.SubscribeStatuseId = Statuseid;
                        model.SubscribtionTypeId = typeid;
                        model.SupplierId = Co.SupplierId;
                        model.Statuse = false;
                        model.EndDate = DateTime.Now.AddDays(Data.NoOfdays);
                        var retun = await _subscribe.AddAsync(model);
                        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                        if (user == null || retun == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        string des = "50 Items , Accept Online Payment with (payfast), 3 - User - Account for every(3 Months)";
                        var redirectUrl = await Pay(retun, Co.SupplierUserName, des, user.Name,user.Surname);
                        return Redirect(redirectUrl);
                        //return View();
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
        [HttpGet]
        public async Task<IActionResult> StandardY()
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
                    Guid typeid = new Guid("ACE9AF2D-AC01-4F3E-9083-08D9A36AA8F5");
                    Guid Statuseid = new Guid("C81FFDD0-25D8-4F9C-E9A8-08D97ADC4CFF");
                    var Data = await _subscribtionType.GetByIdAsync(typeid);
                    if (Data != null)
                    {
                        SubscribeModel model = new SubscribeModel();
                        model.AmountPayed = Data.Price;
                        model.DateSubscribe = DateTime.Now;
                        model.SubscribeStatuseId = Statuseid;
                        model.SubscribtionTypeId = typeid;
                        model.SupplierId = Co.SupplierId;
                        model.Statuse = false;
                        model.EndDate = DateTime.Now.AddDays(Data.NoOfdays);
                        var retun = await _subscribe.AddAsync(model);
                        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                        if (user == null || retun == null)
                        {
                            return RedirectToAction("Error", "Error");
                        }
                        string des = "50 Items , Accept Online Payment with (payfast) , 3 - User - Account for every(12 Months)";
                        var redirectUrl = await Pay(retun, Co.SupplierUserName, des, user.Name,user.Surname);
                        return Redirect(redirectUrl);
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
        [HttpGet]
        public async Task<IActionResult> Plan()
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
                    Guid id = new Guid("C80874A1-005B-4658-E9A6-08D97ADC4CFF");
                    var Data = await _subscribe.GetBySupplierStatuseIdAsync(Co.SupplierId, id);
                    if (Data == null)
                    {
                        TempData["Free"] = "free Plan";
                        return RedirectToAction("Plan", "Home");
                    }
                    else
                    {
                        TempData["Stand"] = "Stand Plan";
                        return RedirectToAction("Plan", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Cancel(string Id)
        {
            Anelka();
            if (Id!=null)
            {
                Guid id = new Guid(Id);
                Guid Statuseid = new Guid("E467FAB6-FEA4-40F0-E9A7-08D97ADC4CFF");
                SubscribeModel Data =await _subscribe.GetByIdAsync(id);
                Data.SubscribeStatuseId = Statuseid;
                await _subscribe.UpdaAsync(Data);
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Return(string Id)
        {
            Anelka();
            if (Id != null)
            {
                Guid id = new Guid(Id);
                Guid Statuseid = new Guid("C80874A1-005B-4658-E9A6-08D97ADC4CFF");
                Guid supplierStatuseId = new Guid("EA45280F-8811-4D04-559B-08D963E7ACB0");
                Guid supplierType = new Guid("2C15603D-0D14-43FD-28B6-08D963E780C5");
                SubscribeModel Data = await _subscribe.GetByIdAsync(id);
                if(Data==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                Data.SubscribeStatuseId = Statuseid;
                Data.Statuse = true;
                await _subscribe.UpdaAsync(Data);
                var supp = await _supplier.GetByIdAsync(Data.SupplierId);
                if(supp==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                var userName = await _userManager.FindByEmailAsync(supp.SupplierUserName);
                if (userName == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                string Role = "Stand Plan";

                supp.SupplierStatuseId = supplierStatuseId;
                supp.SupplierTypeId = supplierType;

                await _supplier.UpdatAsync(supp);
                await _userManager.AddToRoleAsync(userName, Role);
                await _signInManager.RefreshSignInAsync(userName);
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
        }
        public async Task sandbox(string To, string PaymentId)
        {

            string Subject = "SandBox Account";
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            string Body = "<div><p1> the subscrption was made while pament method was on sandbox mode </p1>" +
                 "<p2>, this means that payment  Id " + PaymentId + "  will not be considered </p2>   ";

            Mail.Body = Body;

            Mail.IsBodyHtml = true;
            Mail.From = new MailAddress("no-reply@sistore.co.za");
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "plesk6000a.trouble-free.net.";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential("no-reply@sistore.co.za", "$Cg9xb86");

            await smtp.SendMailAsync(Mail);
       
        }
        private async Task<string> Pay(SubscribeModel model,string Email,string desctription,string Fn,string Ln)
        {
            Anelka();
            Guid id = new Guid("90852C7A-D237-4CE3-E648-08D9B5E859DF");
            var methode = await _pay.GetByIdAsync(id);
            if (methode == null)
            {
                return "/Error/Error";
            }
           string ProccessUrl = "";
            if (methode.SandBox == true)
            {
                await sandbox(Email, model.SubscribeId.ToString());
                await sandbox("anelesiwela18@gmail.com", model.SubscribeId.ToString());
                ProccessUrl = "https://www.sandbox.payfast.co.za/eng/process?";
            }
            else
            {
                ProccessUrl = "https://www.payfast.co.za/eng/process?";
            }
            string Id = model.SubscribeId.ToString();
            string ReturnUrl = "https://www.sistore.co.za/Payment/Return/" + Id;
            string Cancel_Url = "https://www.sistore.co.za/Payment/Cancel/" + Id;
            var Pay = new PayFastRequest(methode.Passpharse);
            Pay.merchant_id = methode.marchantId;
            Pay.merchant_key = methode.marchantKey;
            Pay.return_url = ReturnUrl;
            Pay.cancel_url = Cancel_Url;

            Pay.email_address = Email;
            Pay.m_payment_id = Id;
            Pay.amount = decimal.ToDouble(model.AmountPayed);
            Pay.item_name = desctription;
            Pay.name_last = Ln;
            Pay.name_first = Fn;

            var Redir = $"{ProccessUrl}{Pay.ToString()}";

            return Redir;


        }
        
    }
}
