using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
namespace OurShop.Controllers
{
    [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
    public class BrandController : Controller
    {

        private readonly IBrand _brand;
        private readonly IContactDetail _detailContact;
        private readonly IResident _resident;
        private readonly IBrandDeatail _brandDeatail;
        private readonly ISupplier _supplier;
        private readonly ICity _city;
        private readonly IUserAccount _userAccount;
        private readonly ILogger<BrandController> _logger;
        public BrandController(ILogger<BrandController> logger, IUserAccount userAccount,ICity city, ISupplier supplier, IBrand brand, IContactDetail detail, IResident resident, IBrandDeatail brandDeatail)
        {
            _city = city;
            _logger = logger;
            _supplier = supplier;
            _brand = brand;
            _userAccount = userAccount;
            _detailContact = detail;
            _resident = resident;
            _brandDeatail = brandDeatail;
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
            Anelka();
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
        public async Task<IActionResult> Index()
        {
            Anelka();
            var Co = await Product(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
           ViewBag.Link = Url.Action("Page", "Product", new { Id = Co.SupplierId }, Request.Scheme);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Brand()
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
                    BrandModel model = await _brand.GetByIdAsync(Co.BrandId);
                    return View(model);
                }
            }
            return RedirectToAction("Error", "Error");

        }
        [HttpGet]
        public async Task<IActionResult> CompanyAddress()
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
                    ResidentModel model = await _resident.GetBySupplIdAsync(Co.SupplierId);
                    ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
                    return View(model);
                }
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Address(ResidentModel model)
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
                    await _resident.UpdaAsync(model);
                    return RedirectToAction("CompanyAddress", "Brand");
                }
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Address()
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
                    ResidentModel model = await _resident.GetBySupplIdAsync(Co.SupplierId);
                    ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
                    return View(model);
                }
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> ContactDetails()
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
                    BrandModel mode = await _brand.GetByIdAsync(Co.BrandId);
                    ContactDetailModel model = await _detailContact.GetByBradIdAsync(mode.BrandId);
                    return View(model);
                }
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Contact()
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
                    BrandModel mode = await _brand.GetByIdAsync(Co.BrandId);
                    ContactDetailModel model = await _detailContact.GetByBradIdAsync(mode.BrandId);
                    return View(model);
                }
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Contact(ContactDetailModel model)
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
                    await _detailContact.UpdatAsync(model);
                    return RedirectToAction("ContactDetails", "Brand");
                }
            }
            return RedirectToAction("Error", "Error");
        }
    }
}
