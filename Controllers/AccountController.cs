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
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace OurShop.Controllers
{           
    public class AccountController : Controller
    {
        private readonly UserManager<UserPlusModel> _userManager;
        private readonly SignInManager<UserPlusModel> _signInManager;
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly ILogger<AccountController> _logger;
        private readonly ICustomer _customer;
        private readonly ISupplier _supplier;
        private readonly IAdmin _admin;
        private readonly IUserAccount _userAccount;
        private readonly IContactDetail _contactDetail;
        private readonly IResident _location;
        private readonly ILoginTrack _loginTrack;
        public AccountController(ILoginTrack loginTrack,ILogger<AccountController> logger, IResident location,IContactDetail contactDetail,IUserAccount userAccount,IAdmin admin,ICustomer customer, ISupplier supplier, RoleManager<IdentityRole> RoleManger, UserManager<UserPlusModel> userManager, SignInManager<UserPlusModel> signInManager)
        {
            _admin = admin;
            _loginTrack = loginTrack;
            _logger = logger;
            _location = location;
            _contactDetail = contactDetail;
            _userAccount = userAccount;
            _customer = customer;
            _supplier = supplier;
            _signInManager = signInManager;
            _userManager = userManager;
            this._RoleManger = RoleManger;
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
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Info()
        {
            return View();
        }
        [HttpGet]
     
        public IActionResult Documentation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Settings()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LoginCall(string returnUrl=null,string remoteError=null)
        {
            Anelka();
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginModel model = new LoginModel
            {
                returnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if(remoteError!=null)
            {
                ModelState.AddModelError(string.Empty, $"Error Accurred from external Account : {remoteError}");
                return View("Login", model);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if(info==null)
            {
                ModelState.AddModelError(string.Empty, $"Error Accurred from external Account");
                return View("Login", model);
            }
            var Email = info.Principal.FindFirstValue(ClaimTypes.Email);
            UserPlusModel user = null;
            if (Email != null)
            {
                user = await _userManager.FindByEmailAsync(Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email Is not Confirmed ");
                    return View("Login", model);
                }
            }

            var sigin = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if(sigin.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
              
                if(Email!=null)
                {
                    if(user==null)
                    {
                        user = new UserPlusModel
                        {
                            Bool = false,
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Surname = info.Principal.FindFirstValue(ClaimTypes.Surname),
                            PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone)

                        };
                        await _userManager.CreateAsync(user);
                    }

                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user,isPersistent:false);

                    string Role = "Buy";
                    await _userManager.AddToRoleAsync(user, Role);
                    CustomerModel Data = new CustomerModel();
                    Data.CustomerUserName = user.Email;
                    await _customer.AddAsync(Data);

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { UserId = user.Id, token }, Request.Scheme);
                    await Send(user.Email, confirmationLink);
                    return RedirectToAction("EmailSendLink", "Account");
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
            }
        }
        [AllowAnonymous]
        [HttpPost]

        public IActionResult ExternalLogin(string provider,string returnUrl)
        {
            Anelka();
            var redirect = Url.Action("LoginCall", "Account", new { returnUrl = returnUrl });
            var propertie =  _signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return new ChallengeResult(provider, propertie);
        
        }
        [HttpGet]

        public IActionResult Passwor()
        {
           
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Passwor(ChangePassword change)
        {
            Anelka();
            if (ModelState.IsValid)
            {
                var Use = await _userManager.GetUserAsync(User);
                var Hash = await _userManager.AddPasswordAsync(Use,change.NewPassword);
                if (Hash.Succeeded)
                {
                    return RedirectToAction("PasswordChanged", "Account");
                }
                else
                {
                    foreach (var error in Hash.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(change);
                }
            }
            return View(change);
        }
        [HttpGet]

        public async Task<IActionResult> ChangePassword()
        {
            Anelka();
            var Use=await _userManager.GetUserAsync(User);
            var Hash = await _userManager.HasPasswordAsync(Use);
            if(!Hash)
            {
                return RedirectToAction("Password", "Account");
            }
            return View();
        }
        [HttpPost]

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel mode)
        {
            Anelka();
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
            Anelka();
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var use = await _userManager.FindByEmailAsync(User.Identity.Name);
                    use.PhoneNumber = model.Phone;
                    var Data = await _userManager.UpdateAsync(use);
                    if (Data.Succeeded)
                    {
                        ViewBag.su = true;
                        return View(model);
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
                    return RedirectToAction("Error", "Error");
                }


            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            Anelka();
            UserEditViewModel Data = new UserEditViewModel();
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                var use = await _userManager.FindByEmailAsync(User.Identity.Name);

                Data.Phone = use.PhoneNumber;

            }
            return View(Data);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            Anelka();
            LoginModel model = new LoginModel
            {
                returnUrl = returnUrl,
                 ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList() 
            };

            return View(model);
        }
       
        public string Password()
        {
            const string letter = "0M0NB5V4CX9ZA1B2C3D5F4G7H8J9L6PIUYTREWQ";
            StringBuilder res = new StringBuilder();
            int z = 6;
            Random rndm = new Random();
            while (0 < z--)
            {
                res.Append(letter[rndm.Next(letter.Length)]);
            }
            return res.ToString();
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult>  Remove(string Id)
        {
            Anelka();
            if (Id==null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                Guid id = new Guid(Id);
                await _userAccount.RemoveAsync(id);
            }
            return RedirectToAction("List", "Account");
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> List()
        {
            Anelka();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            string Role = "Stand Plan";
            if (await _userManager.IsInRoleAsync(user, Role))
            {
                var mysupp = await _supplier.GetByEmailAsync(User.Identity.Name);
                if (mysupp == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                var List = await _userAccount.TabAsync(mysupp.SupplierId);
                List<RegisterModel> model = new List<RegisterModel>();
                foreach(var ist in List)
                {
                    RegisterModel mode = new RegisterModel();
                    var er = await _userManager.FindByEmailAsync(ist.UserName);
                    if(er!=null)
                    {
                        mode.Email = er.Email;
                        mode.Phone = er.PhoneNumber;
                        mode.Name = er.Name;
                        mode.Surname = er.Surname;
                        mode.Id = ist.UserAccountId;
                        model.Add(mode);
                    }
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }

        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Supplier()
        {
            Anelka();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            string Role = "Stand Plan";
            if (await _userManager.IsInRoleAsync(user, Role))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
           
        }
        [HttpGet]
        public IActionResult Notify()
        {
            if(TempData["Data"]!=null)
            {
                ViewBag.Data = TempData["Data"];
                TempData.Clear();
            }
            
            return View();

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Pro Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Supplier(RegisterModel Model)
        {
            Anelka();
            var mysupp = await _supplier.GetByEmailAsync(User.Identity.Name);
            if(mysupp==null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                var checl = await _userAccount.TabAsync(mysupp.SupplierId);
                if(checl.Count()>=2)
                {
                    TempData["Data"] = "You have reach a limit of 2 user";
                    return RedirectToAction("Notify", "Account");
                }
            }
            if (ModelState.IsValid)
            {
               
                var supp = await _supplier.GetByEmailAsync(Model.Email);
                if (supp==null)
                {
                    var user = await _userManager.FindByEmailAsync(Model.Email);

                    if (user != null)
                    {
                        string Role = "Stand Plan";
                        if (await _userManager.IsInRoleAsync(user, Role))
                        {
                            TempData["Data"] = "This User already assign to a role";
                            return RedirectToAction("Notify", "Account");//User Alredy assgn to this role
                        }
                        else
                        {
                            var naa = await _userAccount.GetByEmailAsync(user.Email);
                            if (naa != null)
                            {
                                TempData["Data"] = "This User already assign to a role";
                                return RedirectToAction("Notify", "Account");//User Alredy assgn to this role
                            }
                            else
                            {
                                UserAccount n = new UserAccount();
                                n.SupplierId = mysupp.SupplierId;
                                n.UserName = Model.Email;
                                await _userAccount.AddAsync(n);
                                TempData["Data"] = "Congratulation you have succesfully assign the user . they can login and  switch to sell mode44q";
                                return RedirectToAction("Notify", "Account");//Congratulation You have registed user
                            }
                        }
                    }
                }
                else
                {
                    TempData["Data"] = "You can not assign someone who is alredy have  registed to another brand";
                    return RedirectToAction("Notify", "Account");//User Alredy assgn to this role 
                }


                string password = Password();
                var User = new UserPlusModel { UserName = Model.Email, Email = Model.Email, Bool = true, PhoneNumber = Model.Phone, Name = Model.Name, Surname = Model.Surname };
                var Result = await _userManager.CreateAsync(User, password);
                if (Result.Succeeded)
                {
                    string Role = "Stand Plan";
                    await _userManager.AddToRoleAsync(User, Role);

                    UserAccount n = new UserAccount();
                    n.SupplierId = mysupp.SupplierId;
                    n.UserName = Model.Email;
                    await _userAccount.AddAsync(n);


                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { UserId = User.Id, token }, Request.Scheme);
                    await Sen(Model.Email, confirmationLink, password);
                    TempData["Data"] = "Password and Email confimation link was sent to "+ Model.Email+" . the user can login";
                    return RedirectToAction("Notify", "Account");
                }
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.USuccess = "Somthing Went wrong Try Again..";

            return View(Model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register(string Sell)
        {

            Anelka();


            RegisterUserModel Model = new RegisterUserModel();
            if (Sell == null||Sell=="Buy")
            {
                Model.Is = false;
            }
            else
            {
                Model.Is = true;
            }
            Model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(Model);
        }
        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel Model)
        {
            Anelka();
            if (ModelState.IsValid)
            {
               
                var User = new UserPlusModel { UserName = Model.Email, Email = Model.Email, Bool = Model.Is, PhoneNumber=Model.Phone ,Name=Model.Name,Surname=Model.Surname };
                var Result = await _userManager.CreateAsync(User, Model.Password);
                if (Result.Succeeded)
                {

                    if (User.Bool == true)
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
            Model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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
        public async Task<IActionResult> Login(LoginModel Model)
        {
            Anelka();
            Model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var find = await _userManager.FindByEmailAsync(Model.Email);
                if (find != null && !find.EmailConfirmed && (await _userManager.CheckPasswordAsync(find, Model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email Is not Confirmed Check your Confirmation to your inbox");
                    return View(Model);
                }
                var Result = await _signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.Remember, true);
                if (Result.Succeeded)
                {
                    LoginTrack O = new LoginTrack();
                    O.DateTime = DateTime.Now;
                    O.UserName = Model.Email;
                    await _loginTrack.AddAsync(O);
                    if (!string.IsNullOrEmpty(Model.returnUrl) && Url.IsLocalUrl(Model.returnUrl))
                    {
                       
                        return Redirect(Model.returnUrl);
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
                            var check = await _contactDetail.GetByBradIdAsync(data.BrandId);
                            var loc = await _location.GetBySupplIdAsync(data.SupplierId);
                            if (data.BrandId==idBrand) //Check brand
                            {
                                return RedirectToAction("Brand", "Subscription");
                            }
                            if (data.BrandDeatailId == idDetail)//check details
                            {
                                return RedirectToAction("BrandDeatail", "Subscription");
                            }
                            else if(check==null)
                            {
                                return RedirectToAction("BrandContact", "Subscription");
                            }
                            else if(loc==null)
                            {
                                return RedirectToAction("Location", "Subscription");
                            }
                            else
                            {
                                return RedirectToAction("Welcome", "Account");
                            }
                        }
                        else
                        {
                            //Direct to home of seller
                            return RedirectToAction("Welcome", "Account");
                        }
                      
                    }
                    else
                    {
                        //direct  to home page of Buyres
                        return RedirectToAction("Product", "Product");
                    }

                   
                }
                if(Result.IsLockedOut)
                {
                    return RedirectToAction("Lockout", "Account");
                }
                ModelState.AddModelError(string.Empty, "Login Failed ... Invalid Email or Password ");

            }
            return View(Model);
        }
        public async Task<RedirectToActionResult> Sen(string To, string confirmationLink, string Password)
        {

            string Subject = "SI Email Confimation";
            //string body = confirmationLink;
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            Mail.Body = "<p1>Please Confirm your email <br/> </p1>" + "<p2>Your Login Password &nbsp;</p2>" + Password + "<br/> <hr/>" + "<a href=" + confirmationLink + ">Confirm</a>";
            Mail.IsBodyHtml = true;
            Mail.From = new MailAddress("no-reply@sistore.co.za");
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "plesk6000a.trouble-free.net.";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential("no-reply@sistore.co.za", "$Cg9xb86");

            await smtp.SendMailAsync(Mail);
            return RedirectToAction("EmailSendLink", "Account");


        }
        [AllowAnonymous]

        public async Task<RedirectToActionResult> Send(string To, string confirmationLink)
        {

            string Subject = "SI Store  Email Confimation";
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            Mail.Body = "<p1>Please Confirm your email</p1>" + "<hr/>" + "<a href=" + confirmationLink + ">Confirm</a>";
            Mail.IsBodyHtml = true;
            Mail.From = new MailAddress("no-reply@sistore.co.za");
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "plesk6000a.trouble-free.net.";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential("no-reply@sistore.co.za", "$Cg9xb86");

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
            Anelka();
            if (data.Email == null || data.token == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            var use = await _userManager.FindByEmailAsync(data.Email);
            if (use == null)
            {
                return RedirectToAction("EmailConfimationFailed", "Account");
            }
            var res = await _userManager.IsEmailConfirmedAsync(use);

            if (res==true)
            {
                var xx = await _userManager.ResetPasswordAsync(use, data.token, data.Password);
                if (xx.Succeeded)
                {
                    if(await _userManager.IsLockedOutAsync(use))
                    {
                        await _userManager.SetLockoutEndDateAsync(use, DateTimeOffset.UtcNow);
                    }
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
            Anelka();
            if (ModelState.IsValid)
            {

                var use = await _userManager.FindByEmailAsync(model.Email);

                if (use != null && await _userManager.IsEmailConfirmedAsync(use))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(use);
                    var passwordRestLink = Url.Action("ForgotPasswordReset", "Account", new { email = model.Email, token }, Request.Scheme, Request.Host.ToString());

                    await Send(model.Email, passwordRestLink);
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
            Anelka();
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
