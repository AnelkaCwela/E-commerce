using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Controllers
{
    [AllowAnonymous]
    public class SubscriptionController : Controller
    {
        private readonly IBrand _brand;
        private readonly IContactDetail _detailContact;
        private readonly IResident _resident;
        private readonly IBrandDeatail _brandDeatail;
        private readonly ISupplier _supplier;
        private readonly ICity _city;
        private  byte[] DatAa { get; set; }
        private byte[] file1 { get; set; }
        private byte[] file2 { get; set; }
        public SubscriptionController(ICity city,ISupplier supplier, IBrand brand, IContactDetail detail, IResident resident, IBrandDeatail brandDeatail)
        {
            _city = city;
            _supplier = supplier;
            _brand = brand;
            _detailContact = detail;
            _resident = resident;
            _brandDeatail = brandDeatail;
        }

        [HttpGet]
        public async Task<IActionResult> Reg()
        {
           
            if (User.Identity.Name != null)
            {
                var data = await _supplier.GetByEmailAsync(User.Identity.Name);
                if (data != null)
                {
                    return RedirectToAction("Brand", "Subscription");
                }
                Guid Statuseid = new Guid("1DFF1EFD-66E2-470A-559C-08D963E7ACB0");
                Guid Typeid = new Guid("5395D734-3CDC-4C3B-28B7-08D963E780C5");
                Guid branddeail = new Guid("B416E114-6534-4A31-1658-08D964B95D75");
                Guid brandId = new Guid("EF997AA1-3D23-41CF-3EC0-08D964BD3C34");
                SupplierModel sub = new SupplierModel();
                sub.SupplierStatuseId = Statuseid;
                sub.RegistartionDate = DateTime.Now;
                sub.SupplierTypeId = Typeid;
                sub.BrandId = brandId;
                sub.SupplierUserName = User.Identity.Name;
                sub.BrandDeatailId = branddeail;
               var da= await _supplier.AddAsync(sub);
                if(da!=null)
                {
                   
                    return RedirectToAction("Brand", "Subscription");
                }

            }
            return RedirectToAction("Error", "Home");
        }
        

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Brand(BrandModel model)
        {
            if(ModelState.IsValid)
            {
                if(User.Identity.Name!=null)
                {

                    foreach (var file in Request.Form.Files)
                    {


                        if (file == null)
                        {
                            ViewBag.file = "png or jpn file format required";
                            return View(model);
                        }
                        MemoryStream ms = new MemoryStream();

                        try
                        {
                            file.CopyTo(ms);
                            var supported = new[] { "png", "jpg", "PNG", "JPG" };
                            var f = Path.GetExtension(file.FileName).Substring(1);
                            if (!supported.Contains(f))
                            {
                                ViewBag.file = "png or jpn file format required";
                                return View(model);
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
                        ms.Close();
                        ms.Dispose();
                        break;

                    }

                    model.BrandLogo = DatAa;
                    var dat = await _brand.AddAsync(model);
                    if (dat != null)
                    {

                        SupplierModel data = await _supplier.GetByEmailAsync(User.Identity.Name);
                        if (data == null)
                        {
                            return RedirectToAction("Error", "Home");
                        }
                        data.BrandId = dat.BrandId;
                        await _supplier.UpdatAsync(data);

                        return RedirectToAction("BrandDeatail", "Subscription");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }



                }
            }
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> BrandDeatail(BrandDeatailModel model)
        {
            if(ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    int count = 1;

                    foreach (var file in Request.Form.Files)
                    {

                        MemoryStream ms = new MemoryStream();
                        if (file != null && count == 1)
                        {
                            try
                            {
                                file.CopyTo(ms);
                                var supported = new[] { "pdf", "jpg", "PNG", "JPG", "png" };
                                var f = Path.GetExtension(file.FileName).Substring(1);
                                if (!supported.Contains(f))
                                {
                                    ViewBag.file = "pdf or png or jpn file format required";
                                    return View(model);
                                }
                                else
                                {
                                    file1 = ms.ToArray();
                                }
                            }
                            catch
                            {
                                return RedirectToAction("Error", "Home");
                            }
                        }
                        else if (file != null && count == 2)
                        {
                            try
                            {
                                file.CopyTo(ms);
                                var supported = new[] { "pdf", "jpg", "PNG", "JPG", "png" };
                                var f = Path.GetExtension(file.FileName).Substring(1);
                                if (!supported.Contains(f))
                                {
                                    ViewBag.file = "png or jpn file format required";
                                    return View(model);
                                }
                                else
                                {
                                    file2 = ms.ToArray();
                                }
                            }
                            catch
                            {
                                return RedirectToAction("Error", "Home");
                            }
                        }
                        else if (count < 3)
                        {
                            ViewBag.file = "Please attach both Bussness Registation and Identity document";
                            return View(model);
                        }
                        ms.Close();
                        ms.Dispose();
                        count++;

                    }
                    model.IdentityDocument = file1;
                    model.BussnessRegistration = file2;
                    var dat = await _brandDeatail.AddAsync(model);
                    if (dat != null)
                    {
                        SupplierModel data = await _supplier.GetByEmailAsync(User.Identity.Name);
                        if (data == null)
                        {
                            return RedirectToAction("Error", "Home");
                        }
                        data.BrandDeatailId = dat.BrandDeatailId;
                        await _supplier.UpdatAsync(data);


                        return RedirectToAction("BrandContact", "Subscription");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BrandContact(ContactDetailModel model)
        {
            if(ModelState.IsValid)
            {
                if(User.Identity.Name!=null)
                {
                    SupplierModel data = await _supplier.GetByEmailAsync(User.Identity.Name);
                    if(data==null)
                    {
                        return RedirectToAction("Error", "Home");
                    }
                    model.BrandId = data.BrandId;
                    var d = await _detailContact.GetByBradIdAsync(data.BrandId);
                    if (d==null)
                    {
                        await _detailContact.AddAsync(model);
                    }
                    
                    return RedirectToAction("Location", "Subscription");

                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Location(ResidentModel model)
        {
            if(ModelState.IsValid)
            {
                if (User.Identity.Name != null)
                {
                    var data = await _supplier.GetByEmailAsync(User.Identity.Name);
                    if (data == null)
                    {
                        return RedirectToAction("Error", "Home");
                    }
                    var check = await _resident.GetBySupplIdAsync(data.SupplierId);
                    if(check!=null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    model.SupplierId = data.SupplierId;
                    await _resident.AddAsync(model);
                    //  if(c==null)
                    //Guid id = new Guid("EA45280F-8811-4D04-559B-08D963E7ACB0");
                    //data.SupplierStatuseId = id;
                    //await _supplier.UpdatAsync(data);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Error", "Home");

                }
            }
            ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BrandDeatail()
        {

            if (User.Identity.Name != null)
            {
                var data = await _supplier.GetByEmailAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    Guid branddeail = new Guid("B416E114-6534-4A31-1658-08D964B95D75");                 
                    if (data.BrandDeatailId == branddeail)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("BrandContact", "Subscription");
                    }

                }
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Brand()
        {
            if(User.Identity.Name!=null)
            {
                var data = await _supplier.GetByEmailAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                {                  
                    Guid brandId = new Guid("EF997AA1-3D23-41CF-3EC0-08D964BD3C34");           
                    if(data.BrandId== brandId)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("BrandDeatail", "Subscription");
                    }

                }
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> BrandContact()
        {
            if (User.Identity.Name != null)
            {
                var data = await _supplier.GetByEmailAsync(User.Identity.Name);
             
                if (data == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                var brand = await _brand.GetByIdAsync(data.BrandId);
                Guid brandId = new Guid("EF997AA1-3D23-41CF-3EC0-08D964BD3C34");
                if (brand == null || brand.BrandId == brandId)
                {
                    return RedirectToAction("Brand", "Subscription");
                }
                else
                {
               
                    var isnull=await _detailContact.GetByBradIdAsync(data.BrandId);
                   
                    if (isnull==null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Location", "Subscription");
                    }
                        
                }
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Location()
        {
            if (User.Identity.Name != null)
            {
                var data = await _supplier.GetByEmailAsync(User.Identity.Name);
                if (data == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    var ch = await _resident.GetBySupplIdAsync(data.SupplierId);
                    if (ch==null)
                    {
                        ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
