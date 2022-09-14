using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using OurShop.Models;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using OurShop.Models.Databinding;


namespace OurShop.Controllers
{           
    public class AccountController : Controller
    {
        private readonly UserManager<UserPlusModel> _userManager;
        private readonly SignInManager<UserPlusModel> _signInManager;
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly ICustomer _customer;
        private readonly ISupplier _supplier;
        [Obsolete]
        public AccountController(ICustomer customer, ISupplier supplier, RoleManager<IdentityRole> RoleManger, UserManager<UserPlusModel> userManager, SignInManager<UserPlusModel> signInManager)
        {
            _customer = customer;
            _supplier = supplier;
            _signInManager = signInManager;
            _userManager = userManager;
            this._RoleManger = RoleManger;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
     

        [HttpGet]

        public IActionResult ChangePassword()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }         
            return View();
        }
        [HttpPost]

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel mode)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var use = await _userManager.FindByEmailAsync(User.Identity.Name);
                    if (use == null)
                    {
                        return RedirectToAction("EmailConfimationFailed", "Account");
                    }
                    var res = await _userManager.ChangePasswordAsync(use, mode.CurrentPassord, mode.NewPassword);


                    if (res.Succeeded)
                    {
                        await _signInManager.SignInAsync(use, isPersistent: true);

                        await _signInManager.SignOutAsync();
                        return RedirectToAction("PasswordChanged", "Account");

                    }
                    else
                    {
                        return RedirectToAction("EmailConfimationFailed", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("EmailConfimationFailed", "Account");
                }
            }

            return View();
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var use = await _userManager.FindByEmailAsync(User.Identity.Name);
                    use.Phone = model.Phone;
                   var Data=  await  _userManager.UpdateAsync(use);
                    if(Data.Succeeded)
                    {
                        return RedirectToAction("Profile", "Account");
                    }
                    else
                    {
                        foreach (var error in Data.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("EmailConfimationFailed", "Account");
                }
              

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            UserEditViewModel Data = new UserEditViewModel();
            if (User.Identity.Name == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            else
            {
                var use = await _userManager.FindByEmailAsync(User.Identity.Name);
               
                Data.Phone = use.Phone;
               
            }
            return View(Data);
        }
       


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel Model)
        {           
            if (ModelState.IsValid)
            {
                

                var User = new UserPlusModel { UserName = Model.Email, Email = Model.Email, Bool = true,PhoneNumber=Model.Phone  };
                var Result = await _userManager.CreateAsync(User, Model.Password);
                if (Result.Succeeded)
                {
                    if(User.Bool == true)
                    {
                        string Role = "Sell";
                        await _userManager.AddToRoleAsync(User, Role);
                    }
                    else
                    {
                        string Role = "Buy";
                        await _userManager.AddToRoleAsync(User, Role);
                        CustomerModel Data = new CustomerModel();
                        Data.CustomerUserName = Model.Email;
                       await _customer.AddAsync(Data);
                    }
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { UserId = User.Id, token }, Request.Scheme);
                    await Send(Model.Email, confirmationLink);
                 
                    return RedirectToAction("EmailSendLink", "Account");
                }
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.USuccess = "Somthing Went wrong Try Again..";

            return View(Model);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailIsInUsE(string email)
        {
            var userr = await _userManager.FindByEmailAsync(email);
            if (userr == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel Model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var Result = await _signInManager.PasswordSignInAsync(Model.Email, Model.Password, true, false);
                if (Result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                       
                        return Redirect(ReturnUrl);
                    }

                    var use = await _userManager.FindByEmailAsync(Model.Email);
                    string Seller = "Sell";
                    if (await _userManager.IsInRoleAsync(use, Seller))
                    {
                        var data = await _supplier.GetByEmailAsync(User.Identity.Name);
                        Guid id = new Guid("1DFF1EFD-66E2-470A-559C-08D963E7ACB0");
                        if (data==null)
                        {
                            return RedirectToAction("Reg", "Subscription");
                        }
                        if(data.SupplierStatuseId==id)
                        {
                            Guid idBrand = new Guid("EF997AA1-3D23-41CF-3EC0-08D964BD3C34");
                            Guid idDetail = new Guid("B416E114-6534-4A31-1658-08D964B95D75");
                            if (data.BrandId==idBrand) //Check brand
                            {
                                return RedirectToAction("Brand", "Subscription");
                            }
                            if (data.BrandDeatailId == idDetail)//check details
                            {
                                return RedirectToAction("BrandDeatail", "Subscription");
                            }
                            else
                            {
                                return RedirectToAction("BrandContact", "Subscription");
                            }
                        }
                        else
                        {
                            //Direct to home of seller
                            return RedirectToAction("Index", "Home");
                        }
                      
                    }
                    else
                    {
                        //direct  to home page of Buyres
                        return RedirectToAction("", "");
                    }

                   
                }

                ModelState.AddModelError(string.Empty, "Login Failed ... Invalid Email or Password ");

            }
            return View(Model);
        }
        [AllowAnonymous]

        public async Task<RedirectToActionResult> Send(string To, string confirmationLink)
        {

            string Subject = "Lost and Found Email Confimation";
            //string body = confirmationLink;
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            Mail.Body = "<p1>Please Confirm your email</p1>" + "<hr/>" + "<a href=" + confirmationLink + ">Confirm</a>";
            Mail.IsBodyHtml = true;
            Mail.From = new MailAddress("infoslostfound@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;

            smtp.Credentials = new System.Net.NetworkCredential("infoslostfound@gmail.com", "Password");

            await smtp.SendMailAsync(Mail);
            return RedirectToAction("EmailSendLink", "Account");


        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgortPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ForgotPasswordReset(string Email, string token)
        {
            if (Email == null || token == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            var use = await _userManager.FindByEmailAsync(Email);
            if (use == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            else
            {
                ForgotPasswordResetViewModel data = new ForgotPasswordResetViewModel();
                data.Email = Email;
                data.token = token;
                return View(data);
            }

        }
        [AllowAnonymous]
        [HttpPost]
        
        public async Task<IActionResult> ForgotPasswordReset(ForgotPasswordResetViewModel data)
        {
            if (data.Email == null || data.token == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            var use = await _userManager.FindByEmailAsync(data.Email);
            if (use == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            var res = await _userManager.ConfirmEmailAsync(use, data.token);

            if (res.Succeeded)
            {
                var xx = await _userManager.ResetPasswordAsync(use, data.token, data.Password);
                if (xx.Succeeded)
                {
                    await _signInManager.SignInAsync(use, isPersistent: true);

                    await _signInManager.SignOutAsync();
                    return RedirectToAction("PasswordChanged", "Account");
                }

                return RedirectToAction("EmailConfimationFailed", "Account"); ;
            }
            else
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }

        }
        [AllowAnonymous]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ForgortPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                var use = await _userManager.FindByEmailAsync(model.Email);

                if (use != null && await _userManager.IsEmailConfirmedAsync(use))
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(use);
                    var passwordRestLink = Url.Action("ForgotPasswordReset", "Account", new { email = model.Email, token }, Request.Scheme, Request.Host.ToString());

                    await Send(model.Email, passwordRestLink);
                    //await _emailService.SendAsync(model.Email, "Email Verification", $"<a href=\"{passwordRestLink}></a>", true);
                    return RedirectToAction("EmailSendLink", "Account");
                }
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            return View(model);
        }
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> ConfirmEmail(string UserId, string token)
        {
            if (UserId == null || token == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");

            }
             UserPlusModel use = await _userManager.FindByIdAsync(UserId);
            if (use == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            var res = await _userManager.ConfirmEmailAsync(use, token);
            if (res.Succeeded)
            {
                await _signInManager.SignInAsync(use, isPersistent: true);
              
                string Seller = "Sell";
                if (await _userManager.IsInRoleAsync(use, Seller))
                {
                    return RedirectToAction("Reg", "Subscription");
                }
                await _signInManager.SignOutAsync();
                return RedirectToAction("ConfirmSuccesfull", "Account");
            }
            else
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmailConfimationFailed()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult PasswordChanged()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmailSendLink()
        {
            return View();
        }
        
              [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmSuccesfull()
        {
            return View();
        }
    }
}
