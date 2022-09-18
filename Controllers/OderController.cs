using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.Databinding;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using PayFast;
using PayFast.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using System.Net.Mail;
using System.IO;
using Microsoft.AspNetCore.Identity;
using OurShop.Models;
using iText.Kernel.Colors;
using Microsoft.Extensions.Logging;

namespace OurShop.Controllers
{
    public class OderController : Controller
    {
        private readonly IContactDetail _contactDetail;
        private readonly IOnlinePayMent _onlinePayMent;
        private readonly ICashOnDe _cashOnDe;
        private readonly IBankType _bankType;
        private readonly IDirectbank _directbank;
        private readonly ISupplier _supplier;
        private readonly IPayment _payment;
        private readonly IImage _image;
        private readonly IDelivery _delivery;
        private readonly IRefrance _refrance;
        private readonly ISize _size;
        private readonly IGander _gander;
        private readonly IColor _color;
        private readonly ICategory _categoryResp;
        private readonly IType _type;
        private readonly IIteam _iteam;
        private readonly ICustomer _customer;
        private readonly IIteamStatuse _iteamStatuse;
        private readonly IIteamDetail _iteamDetail;
        private readonly IBrand _brand;
        public readonly ICity _city;
        private readonly ISubub _subub;
        private readonly ILogger<OderController> _logger;
        private readonly IQoutationDetailz _qoutationDetailz;
        private readonly IQoutation _qoutation;
        private readonly ILike _like;
        public readonly IDeliveryStatuse _deliveryStatuse;
        private readonly ISubscribe _subscribe;
        private readonly IPaymentType _paymentType;
        private readonly IQoutationStatuse _qoutationStatuse;
        private readonly UserManager<UserPlusModel> _UserManger;
        private readonly IUserAccount _userAccount;
        private readonly IWhastapp _whastapp;
        public OderController(ILogger<OderController> logger, IWhastapp whastapp,IUserAccount userAccount,UserManager<UserPlusModel> UserManger,IQoutationStatuse qoutationStatuse,IPaymentType paymentType,IContactDetail contactDetail,ISubscribe subscribe, IDirectbank directbank, IBankType bankType, ICashOnDe cashOnDe, IOnlinePayMent onlinePayMent, IPayment payment, IDeliveryStatuse deliveryStatuse,IDelivery delivery,ILike like,IRefrance refrance,IQoutation qoutation,IQoutationDetailz qoutationDetailz,ISubub subub,ICity city,IBrand brand, IIteamDetail iteamDetail, IIteamStatuse iteamStatuse, ISupplier supplier, ICustomer customer, IImage image, ISize size, IGander gander, IColor color, ICategory categoryResp, IType Typep, IIteam iteam)
        {
            _qoutationStatuse = qoutationStatuse;
             _logger = logger;
            _userAccount = userAccount;
            _whastapp = whastapp;
            _UserManger = UserManger;
            _paymentType = paymentType;
            _subscribe = subscribe;
            _contactDetail = contactDetail;
            _deliveryStatuse = deliveryStatuse;
            _delivery = delivery;
            _directbank = directbank;
            _bankType = bankType;
            _cashOnDe = cashOnDe;
            _onlinePayMent = onlinePayMent;
            _payment = payment;
            _like = like;
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
       private static readonly string DEST = "wwwroot/Oders/Data.pdf";
        private static readonly string LOGO = "wwwroot/Images/Logo1.jpg";
        public void Anelka()
        {
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");

        }
        public async Task<byte[]> Print(Guid Id)
        {
            Anelka();
            QuotationModel qo = await _qoutation.GetByIdAsync(Id);
            SububModel Sub = await _subub.GetByIdAsync(qo.SubrbId);
            PaymentTypeModel pay = await _paymentType.GetByIid(qo.PaymentTypeId);
            QoutationStatuseModel Sta = await _qoutationStatuse.GetByIdAsync(qo.QoutationStatuseId);
            RefranceModel Refranc = await _refrance.GetById(qo.QoutationId);
            List<QuotationDetailModel> Details = (List<QuotationDetailModel>)await _qoutationDetailz.TabbyIdAsync(qo.QoutationId);
            //
            
            //
            PdfWriter writer = new PdfWriter(DEST);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            
            document.SetMargins(20, 20, 20, 20);
            //document.SetFontFamily(StandardFontFamilies.TIMES);
           
            //Image logo = new Image(ImageDataFactory.Create(LOGO));
            //logo.AddStyle(le)
            //Image logo = new Image(ImageType())
            Image img = new Image(ImageDataFactory
               .Create(LOGO))
               .SetTextAlignment(TextAlignment.CENTER);
            document.Add(img);
            Paragraph para = new Paragraph().Add(img);
            para.SetWidth(15);
            para.SetHeight(15);
            para.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            para.SetVerticalAlignment(VerticalAlignment.TOP);
            document.Add(para);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Paragraph title = new Paragraph("Oder Refrance :" + Refranc.Refrance + "").SetFontSize(20).SetFont(bold).SetTextAlignment(TextAlignment.CENTER);
            document.Add(title);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Delivery Address").SetFont(bold).SetFontSize(14));
            document.Add(new Paragraph("Suburb :" + Sub.Suburb).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph("Addessline 1: " + Sub.Address2).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph("Addresline 2: " + Sub.Address2).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph("City: " + Sub.City).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph("Post Code: " + Sub.PostCode).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph("Additional delivery Infomation: " + Sub.Additionalinfomation).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph("Cell Number: " + Sub.Phone).SetFontSize(12).SetFont(font));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Payment Type :" + pay.PaymentType).SetFont(bold).SetFontSize(14));
            document.Add(new Paragraph("Oder Date :" + qo.QoutationDate).SetFont(bold).SetFontSize(14));
            document.Add(new Paragraph(" "));

            Table tab = new Table(UnitValue.CreatePercentArray(new float[] { 1 })).UseAllAvailableWidth();
            Style styleCells = new Style().SetBackgroundColor(ColorConstants.CYAN); //add color to main headers
            tab.AddHeaderCell(new Cell().Add(new Paragraph("Oder Details ").SetFontSize(16).SetFont(bold).SetTextAlignment(TextAlignment.CENTER).AddStyle(styleCells)));
            document.Add(tab);         
            Style styleCell = new Style().SetBackgroundColor(ColorConstants.GREEN).SetFont(bold);

            document.Add(new Paragraph(" "));
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 5,4, 3,2 })).UseAllAvailableWidth();
            table.AddHeaderCell(new Cell().Add(new Paragraph("No Of Items").AddStyle(styleCell)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Unit Price").AddStyle(styleCell)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Price").AddStyle(styleCell)));
            double total = 0;
            foreach (var c in Details)
            {

                table.AddCell(new Cell().Add(new Paragraph(c.Quantty.ToString())).SetFont(font));
                table.AddCell(new Cell().Add(new Paragraph(c.UnitPrice.ToString())).SetFont(font));
                table.AddCell(new Cell().Add(new Paragraph((c.UnitPrice*c.Quantty).ToString())).SetFont(font));
                total = total + (double)(c.UnitPrice * c.Quantty);
            }
            table.AddFooterCell(new Cell().Add(new Paragraph("Total :" + total.ToString())).SetFont(font));     
            document.Add(table);
            document.Close();
          return  System.IO.File.ReadAllBytes(DEST);

        }
        public async Task Sen(string To)
        {

            string Subject ="Oder Details";
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            string Body = "<div><p1> Your payment was Unsuccessfull oder </p1>"+
                 "<p2>Please Contact Store to make arrangement</p2> ";
            
            Mail.Body = Body;

            Mail.IsBodyHtml = true;
            Mail.From = new MailAddress("no-reply@sistore.co.za");
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "*****";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential("no-reply@sistore.co.za", "Password");

            await smtp.SendMailAsync(Mail);
        }
        public async Task sandbox(string To,string PaymentId, string refr)
        {

            string Subject = "SandBox Account";
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            string Body = "<div><p1> the oder was made while pament method was on sandbox mode </p1>" +
                 "<p2>, this means that payment  Id "+PaymentId+ " and oder  with the refrance "+refr+" , will not be considered </p2>   ";

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
        public async Task Send(Guid OderId, string To, string OderNo, string OderLink, string store)
        {

            string Subject = store + " Oder Details";
            MailMessage Mail = new MailMessage();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            string Body = "<div><p1> Your new oder at " + store + "</p1>" +
                 "<p2>Refrance No " + OderNo + "</p2> <p4> <a href=" + OderLink + ">Statuse</a> </p4> </div>";

            Mail.Body = Body;

            Mail.IsBodyHtml = true;
            Mail.Attachments.Add(new Attachment(new MemoryStream(await Print(OderId)), "" + store + " Oder.pdf"));

            Mail.From = new MailAddress("no-reply@sistore.co.za");
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "*********";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential("no-reply@sistore.co.za", "Password");

            await smtp.SendMailAsync(Mail);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Bank(string returnUrl, string Id, string i)//Pay with card
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                if (Id == null || i == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                QuotationModel Quotation = new QuotationModel();
                Guid QoutationStatuseSuplier = new Guid("D1E0E05E-07A7-48EA-B253-08D9760DC8E7");
                Guid QoutationStatuse = new Guid("486983A8-CBEA-4337-69FF-08D9760E73BA");
                Guid OderType = new Guid("D896AF7B-166D-4B7E-F09E-08D9760EA89E");
                Guid PaymentType = new Guid("14773336-203B-4D08-B2DF-08D9760E0E43");
                Guid Subrb = new Guid(Id);
                Guid brand = new Guid(i);
                Cart cart = GetCart();
                if (cart.Lines.Count() == 0)
                {
                    TempData["Data"] = "Cart is emty";
                    return RedirectToAction("Index", "Cart", new { returnUrl = returnUrl });
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
                decimal num = 0;
                var delvey = await _delivery.GetBySupplIdAsync(brand);
                if(delvey!=null)
                {
                    num = delvey.Price;
                }

                //Calcilate Sum
                decimal sum = cart.Lines.Sum(e => e.Data.Price * e.Quantity) + num;
                Quotation.QoutationDate = DateTime.Now;
                Quotation.TotalQoutationPrice = sum;
                Quotation.OderTypeId = OderType;
                Quotation.PaymentTypeId = PaymentType;
                Quotation.QoutationStatuseId = QoutationStatuse;
                Quotation.QoutationStatuseSuplierId = QoutationStatuseSuplier;
                Quotation.SubrbId = Subrb;
                Quotation.CustomerId = Cus.CustomerId;
                var quotsucc = await _qoutation.AddAsync(Quotation);
                string d = await RefranceAsync();
                if (quotsucc == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                RefranceModel Da = new RefranceModel();
                Da.Refrance = d;
                Da.QoutationId = quotsucc.QoutationId;
                await _refrance.Add(Da);
                foreach (var Data in cart.Lines.Where(o => o.id == brand))
                {
                    if (Data != null)
                    {

                        QuotationDetailModel model = new QuotationDetailModel();
                        model.Quantty = Data.Quantity;
                        model.QoutationId = quotsucc.QoutationId;
                        model.UnitPrice = Data.Data.Price;
                        model.IteamDetailId = Data.Iteam.IteamDetailId;

                        var oder = await _qoutationDetailz.AddAsync(model);

                        if (oder != null)
                        {
                            await Update_StockDetail(Data.Iteam.IteamDetailId, Data.Quantity);
                            await RemoveFromCart(Data.Iteam.IteamDetailId.ToString());
                        }
                    }


                }
                var supplier = await _supplier.GetByIdAsync(brand);
                var bran = await _brand.GetByIdAsync(supplier.BrandId);
                if (bran != null)
                {
                    var details = await _contactDetail.GetByBradIdAsync(bran.BrandId);
                    if(details!=null)
                    {
                        ViewBag.Contact = "More infomation  about an oder ,Please   Contact " + details.TelNo + " Or Email" + details.EmailAddress + " ";
                    }
                    
                    ViewBag.Logo = bran.BrandLogo;
                    ViewBag.Name = bran.BrandName;
                }
                var Link = Url.Action("Ite", "Oders", new { Id = quotsucc.QoutationId }, Request.Scheme);
                ViewBag.re = d;
                ViewBag.Link = Link;
                await Send(quotsucc.QoutationId, User.Identity.Name, d, Link, bran.BrandName);
                await Send(quotsucc.QoutationId, supplier.SupplierUserName, d, Link, bran.BrandName);
                // await Send(quotsucc.QoutationId, User.Identity.Name, d, Link, bran.BrandName, bran.BrandLogo);
                List<BankTypeModel>  Dar= await Indexx(brand);
                
                return View(Dar);
            }
            return RedirectToAction("Error", "Error");
            //
        }
        public async Task<List<BankTypeModel>> Indexx(Guid Id)
        {
           
            List<BankTypeModel> model = new List<BankTypeModel>();
            var payment = await _payment.GetBySupplieIdAsync(Id);
            if(payment!=null)
            {
                var dire = await _directbank.GetByPayIdAsync(payment.PaymentId);
                if (dire!=null||dire.Active==true)
                {
                    model = (List<BankTypeModel>)await _bankType.TabByDirectIdAsync(dire.DirectbankId); 
                }
                else
                {
                    model = null;
                }

            }
            else
            {
                model = null;
            }
            return model;

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> NotSet(string returnUrl, string Id, string i)//Pay with card
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                if (Id == null || i == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                QuotationModel Quotation = new QuotationModel();
                Guid QoutationStatuseSuplier = new Guid("D1E0E05E-07A7-48EA-B253-08D9760DC8E7");
                Guid QoutationStatuse = new Guid("486983A8-CBEA-4337-69FF-08D9760E73BA");
                Guid OderType = new Guid("D896AF7B-166D-4B7E-F09E-08D9760EA89E");
                Guid PaymentType = new Guid("14773336-203B-4D08-B2DF-08D9760E0E43");
                Guid Subrb = new Guid(Id);
                Guid brand = new Guid(i);
                Cart cart = GetCart();
                if (cart.Lines.Count() == 0)
                {
                    TempData["Data"] = "Cart is emty";
                    return RedirectToAction("Index", "Cart", new { returnUrl = returnUrl });
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
                decimal num = 0;
                var delvey = await _delivery.GetBySupplIdAsync(brand);
                if (delvey != null)
                {
                    num = delvey.Price;
                }
                //Calcilate Sum
                decimal sum = cart.Lines.Sum(e => e.Data.Price * e.Quantity) + num;
                Quotation.QoutationDate = DateTime.Now;
                Quotation.TotalQoutationPrice = sum;
                Quotation.OderTypeId = OderType;
                Quotation.PaymentTypeId = PaymentType;
                Quotation.QoutationStatuseId = QoutationStatuse;
                Quotation.QoutationStatuseSuplierId = QoutationStatuseSuplier;
                Quotation.SubrbId = Subrb;
                Quotation.CustomerId = Cus.CustomerId;
                var quotsucc = await _qoutation.AddAsync(Quotation);
                string d = await RefranceAsync();
                if (quotsucc == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                RefranceModel Da = new RefranceModel();
                Da.Refrance = d;
                Da.QoutationId = quotsucc.QoutationId;
                await _refrance.Add(Da);
                foreach (var Data in cart.Lines.Where(o => o.id == brand))
                {
                    if (Data != null)
                    {

                        QuotationDetailModel model = new QuotationDetailModel();
                        model.Quantty = Data.Quantity;
                        model.QoutationId = quotsucc.QoutationId;
                        model.UnitPrice = Data.Data.Price;
                        model.IteamDetailId = Data.Iteam.IteamDetailId;

                        var oder = await _qoutationDetailz.AddAsync(model);

                        if (oder != null)
                        {
                            await Update_StockDetail(Data.Iteam.IteamDetailId, Data.Quantity);
                            await RemoveFromCart(Data.Iteam.IteamDetailId.ToString());
                        }
                    }


                }
                var supplier = await _supplier.GetByIdAsync(brand);
                var bran = await _brand.GetByIdAsync(supplier.BrandId);
                if (bran != null)
                {
                    var details = await _contactDetail.GetByBradIdAsync(bran.BrandId);
                    if (details != null)
                    {
                        ViewBag.Contact = "More infomation  about an oder ,Please   Contact " + details.TelNo + " Or Email" + details.EmailAddress + " ";
                    }

                    ViewBag.Logo = bran.BrandLogo;
                    ViewBag.Name = bran.BrandName;
                }
                var Link = Url.Action("Ite", "Oders", new { Id = quotsucc.QoutationId }, Request.Scheme);
                ViewBag.re = d;
                ViewBag.Link = Link;
                await Send(quotsucc.QoutationId, User.Identity.Name, d, Link, bran.BrandName);
                await Send(quotsucc.QoutationId, supplier.SupplierUserName, d, Link, bran.BrandName);
                return View();
            }
            return RedirectToAction("Error", "Error");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(string returnUrl, string Id, string i)//Pay with card
        {
            Anelka();
            if (User.Identity.Name != null)
            {
                if (Id == null|| i==null)
                {
                    return RedirectToAction("Error", "Error");
                }
                QuotationModel Quotation = new QuotationModel();
                Guid QoutationStatuseSuplier = new Guid("D1E0E05E-07A7-48EA-B253-08D9760DC8E7");
                Guid QoutationStatuse = new Guid("486983A8-CBEA-4337-69FF-08D9760E73BA");
                Guid OderType = new Guid("D896AF7B-166D-4B7E-F09E-08D9760EA89E");
                Guid PaymentType = new Guid("A1DF485C-7993-413B-B2DE-08D9760E0E43");
                Guid Subrb = new Guid(Id);
                Guid brand = new Guid(i);
                Cart cart = GetCart();
                if (cart.Lines.Count() == 0)
                {
                    TempData["Data"] = "Cart is emty";
                    return RedirectToAction("Index", "Cart", new { returnUrl = returnUrl });
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
                decimal num = 0;
                var delvey = await _delivery.GetBySupplIdAsync(brand);
                if (delvey != null)
                {
                    num = delvey.Price;
                }
                //Calcilate Sum
                decimal sum = cart.Lines.Sum(e => e.Data.Price * e.Quantity) + num;
                Quotation.QoutationDate = DateTime.Now;
                Quotation.TotalQoutationPrice = sum;
                Quotation.OderTypeId = OderType;
                Quotation.PaymentTypeId = PaymentType;
                Quotation.QoutationStatuseId = QoutationStatuse;
                Quotation.QoutationStatuseSuplierId = QoutationStatuseSuplier;
                Quotation.SubrbId = Subrb;
                Quotation.CustomerId = Cus.CustomerId;
                Quotation.Id = brand;
                var quotsucc = await _qoutation.AddAsync(Quotation);
                string d = await RefranceAsync();
                if (quotsucc == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                RefranceModel Da = new RefranceModel();
                Da.Refrance = d;
                Da.QoutationId = quotsucc.QoutationId;
                await _refrance.Add(Da);
                foreach (var Data in cart.Lines.Where(o => o.id == brand))
                {
                    if (Data != null)
                    {

                        QuotationDetailModel model = new QuotationDetailModel();
                        model.Quantty = Data.Quantity;
                        model.QoutationId = quotsucc.QoutationId;
                        model.UnitPrice = Data.Data.Price;
                        model.IteamDetailId = Data.Iteam.IteamDetailId;

                        var oder = await _qoutationDetailz.AddAsync(model);                        
                        
                        if (oder != null)
                        {
                            await Update_StockDetail(Data.Iteam.IteamDetailId, Data.Quantity);
                            await  RemoveFromCart(Data.Iteam.IteamDetailId.ToString());
                        }
                    }
                    

                }

                var sup = await _supplier.GetByIdAsync(brand);
               
                if (sup == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                
                    var bran = await _brand.GetByIdAsync(sup.BrandId);

                
                var Link = Url.Action("Ite", "Oders", new { Id = quotsucc.QoutationId }, Request.Scheme);
                await Send(quotsucc.QoutationId, User.Identity.Name, d, Link, bran.BrandName);
                await Send(quotsucc.QoutationId, sup.SupplierUserName, d, Link, bran.BrandName);
                var supp = await _UserManger.FindByEmailAsync(User.Identity.Name);
                var url =await Ind(sup.SupplierUserName, brand,quotsucc.QoutationId,decimal.ToDouble(sum), supp.Name,supp.Surname, supp.Email, d);
               return Redirect(url);
            }
            return RedirectToAction("Error", "Error");
            //
        }
        public async Task RemoveFromCart(string Id)
        {
            IteamDetailModel product = new IteamDetailModel();
            if (Id != null)
            {
                Guid id = new Guid(Id);
                product = await _iteamDetail.GetById(id);
            }
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Cancel(string Id)
        {
            Anelka();
            if (Id!=null)
            {
                Guid id = new Guid(Id);
                QuotationModel Output =await _qoutation.GetByIdAsync(id);
                if(Output!=null)
                {
                   
                    Guid Statuse = new Guid("6C489A00-BCC8-4DBF-6A00-08D9760E73BA");
                    Guid PaymentStatuse = new Guid("ADF0E5EA-32D4-4069-F09F-08D9760EA89E");
                    Output.QoutationStatuseId = Statuse;
                    Output.OderTypeId = PaymentStatuse;
                    await _qoutation.UpdaStatuseAsync(Output);
                    await Sen(User.Identity.Name);
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
                return View();
            }
            return RedirectToAction("Error", "Error");
        }
        public IActionResult Onpro()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult>  Returnn(string Id)
        {
            Anelka();
            if (Id != null)
            {
                Guid id = new Guid(Id);
                QuotationModel Output = await _qoutation.GetByIdAsync(id);
                if (Output != null)
                {
                    Guid Statuse = new Guid("9DA0B3D4-1671-48F3-69FE-08D9760E73BA");
                    Guid PaymentStatuse = new Guid("41ABEE3C-61CF-42DC-FDF4-08D997AFE0C0");
                    Output.QoutationStatuseId = Statuse;
                    Output.OderTypeId = PaymentStatuse;
                    await _qoutation.UpdaStatuseAsync(Output);                   
                }
                else
                {
                    return RedirectToAction("Error", "Error");
                }
                return View();
            }
            return RedirectToAction("Error", "Error");
        }
        public async Task<string> Ind(string supemail,Guid SupplierId, Guid QoutationId,double Amount,string Fn,string Ln,string Email,string refrance)
        {
            Anelka();
            string ReturnUrl = "https://www.sistore.co.za/Oder/Returnn/" + QoutationId;
            string Cancel_Url = "https://www.sistore.co.za/Oder/Cancel/" + QoutationId;
            string ProccessUrl ="";
            PaymentModel PayModel = await _payment.GetBySupplieIdAsync(SupplierId);
            OnlinePayMent Online = await _onlinePayMent.GetByPayIdAsync(PayModel.PaymentId);
            if (Online.SandBox==true)
            {
              await  sandbox(Email, QoutationId.ToString(), refrance);
                await sandbox(supemail, QoutationId.ToString(), refrance);

                ProccessUrl = "https://www.sandbox.payfast.co.za/eng/process?";
            }
            else
            {
               ProccessUrl = "https://www.payfast.co.za/eng/process?";
            }
            var Pay = new PayFastRequest(Online.Passpharse);
            Pay.merchant_id = Online.marchantId;
            Pay.merchant_key = Online.marchantKey;
            Pay.return_url = ReturnUrl;
            Pay.cancel_url = Cancel_Url;
         
            Pay.email_address = Email;
            Pay.m_payment_id = QoutationId.ToString();            
            Pay.amount = Amount;
            Pay.item_name = "Refrance : "+ refrance;
            Pay.name_first = Fn;
            Pay.name_last = Ln;
            var Redir = $"{ProccessUrl}{Pay.ToString()}";
        
            return Redir;
 
        }
       
        
        public async Task<IActionResult> Like(string Id,bool Like)
        {
            LikeModel like = new LikeModel();
            if (User.Identity.Name != null)
            {
                Guid id = new Guid(Id);
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
                var Data = await _like.CheckLikeAsync(id, Cus.CustomerId);
                if(Data==null)
                {
                    like.CustomerId = Cus.CustomerId;
                    like.IteamId = id;
                    like.Like = Like;
                  await _like.LikeAsync(like);
                }
                else
                {
                    like.CustomerId = Cus.CustomerId;
                    like.IteamId = id;
                    like.Like = Like;
                    await _like.UnLikeAsync(like);
                }

            }
            else
            {
                like.Like = Like;
            }

            return Json(like);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Info(SububModel model,string id)
        {
            if(ModelState.IsValid)
            {
                 var data=await _subub.AddAsync(model);
                if(data!=null)
                {
                    string Id = data.SubrbId.ToString();
                    return RedirectToAction("CheckOut", "Oder", new { Id=Id,i= id });
                }
                else
                {
                    ViewBag.aller = "Somthing went Wrong please enter deatils again";
                }

            }
            ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
            return View();
        }
        public async Task<IActionResult> Info(string returnUrl,string id)
        {           
                Cart cart = GetCart();
            if (cart.Lines.Count() == 0)
            {
                return RedirectToAction("Index", "Cart",new { returnUrl= returnUrl });
            }
            ViewBag.CityId = new SelectList(await _city.TabAsync(), "CityId", "City");
            ViewBag.id = id;
            return View();
        }


        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        [HttpGet]
        [Route("Oder/CheckOut/{Id}/{i}")]
        public async Task<IActionResult> CheckOut(string Id, string i)
        {
            Anelka();
            if (Id==null|| i==null)
            {
                return RedirectToAction("Info", "Oder");
            }
            Guid SupplierId = new Guid(i);
            var Supplie = await _supplier.GetByIdAsync(SupplierId);
            if(Supplie == null)
            {
                return RedirectToAction("Error", "Error");
            }
            string online = null;
            string card = null;
            var payment =  await _payment.GetBySupplieIdAsync(SupplierId);
            if(payment==null)
            {
                card = "No diposite bank payment has been set for this store";
                online = "No Online payment set for this Store";
            }
            else
            {
                Guid id = new Guid("C80874A1-005B-4658-E9A6-08D97ADC4CFF");
                var Data = await _subscribe.GetBySupplierStatuseIdAsync(SupplierId, id);
                var direct = await _directbank.GetByPayIdAsync(payment.PaymentId);
                if (Data == null)
                {
                   
                    if(direct.Active==false||direct==null)
                    {
                        card = "No diposite bank payment has been set for this store";
                    }
                    online = "No Online payment set for this Store";
                }
                else
                {
                   
                    var onlinePay = await _onlinePayMent.GetByPayIdAsync(payment.PaymentId);
                    if(onlinePay==null||onlinePay.Statuse==false)
                    {
                        online = "No Online payment set for this Store";
                    }
                    if (direct.Active == false || direct == null)
                    {
                        card = "No diposite bank payment has been set for this store";
                    }
                }
            }
            ///Delivery
            ///
            /// 

            var delvey = await _delivery.GetBySupplIdAsync(Supplie.SupplierId);
            if(delvey!=null)
            {
                if (delvey.Active == true)
                {
                    var op = await _deliveryStatuse.GetByIdAsync(delvey.DeliveryStatuseId);
                    if(op!=null)
                    {
                        ViewBag.DeliveryDe = delvey.DeliveryDe;
                        ViewBag.Location = delvey.Location;
                        ViewBag.Price = delvey.Price;
                    }
                   
                }
                else
                {
                    ViewBag.De = null;
                }
            }
            else
            {
                ViewBag.De = null;
            }
            ViewBag.Phone = await What();

            /// 
            /// 
            ///





            var brand = await _brand.GetByIdAsync(Supplie.BrandId);
            if(brand==null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                ViewBag.Logo =brand.BrandLogo;
                ViewBag.Name = brand.BrandName;
            }
            if (card!=null&&online!=null)
            {
                var details = await _contactDetail.GetByBradIdAsync(brand.BrandId);
                if(details==null)
                {
                    ViewBag.Contact = "The are no payment set for this store , and no Contact detials set for this store";
                }
                else
                {
                    ViewBag.Contact = "The are no payment set for this store ,Please Place Oder and Contact " + details.TelNo + " Or Email" + details.EmailAddress + " About payments";
                }
               
            }
            ViewBag.i = i;
            ViewBag.Online = online;
            ViewBag.card = card;
            return View(new CartIndexViewModel
            {

                Cart = GetCart(),
                Id = Id
            });
        }
        public async Task<string> What()
        {
            var sup = await Product(User.Identity.Name);
            if (sup == null)
            {
                return null;
            }

            var whap = await _whastapp.GetBySupplIdAsync(sup.SupplierId);
            if (whap == null)
            {
                return null;
            }

            return whap.Phon;
        }
        public async Task Update_StockDetail(Guid IteamDetailId, int NoOfIteam)
        {
            Anelka();
            var item = await _iteamDetail.GetById(IteamDetailId);
            
            if (item != null )
            {
                var Dat = await _iteam.GetByIdAsync(item.IteamId);

                if (Dat != null)
                {
                    if (NoOfIteam > 0)
                    {
                        IteamDetailModel T1 = new IteamDetailModel();
                        int Num = item.NoOfIteam - NoOfIteam;
                        T1.NoOfIteam = Num;
                        T1.IteamDetailId = item.IteamDetailId;
                        if (Num <= 0)
                        {
                           // await _iteamDetail.Remove(T1.IteamDetailId);
                            await _iteamDetail.UpdaItem(T1.IteamDetailId, 0, false);
                            // await _iteamDetail.GetListByItemIdsync(item.IteamId).
                           var Data= await _iteamDetail.GetListByIteStockIdsync(item.IteamId, true);
                            if(Data==null)
                            {
                                await _iteam.UpdatStocksync(item.IteamId, false);
                            }

                        }
                        else
                        {
                            await _iteamDetail.UpdaItem(T1.IteamDetailId, Num, true);
                        }
                    }
                }
                
               
            }
        }
        [HttpGet]
        public IActionResult Pay(string Id)
        {
            
            return View();

        }

        [HttpGet]
        public IActionResult OderStatuse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OderStatuse(string refrance)
        {
          var  data = await _refrance.GetById(refrance);
            await _qoutation.GetByIdAsync(data.QoutationId);
            //QuotationModel Quotation = new QuotationModel();
            return View();
        }
       [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id!=null)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                Guid Id = new Guid(id);
                var deli = await _delivery.GetByIdAsync(Id);
                ViewBag.DeliveryStatuseId = new SelectList(await _deliveryStatuse.TabAsync(), "DeliveryStatuseId", "StatuseDelivery");
                return View(deli);
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
           
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Edit(DeliveryModel model)
        {
            if (ModelState.IsValid)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                await _delivery.UpdatAsync(model);
                return RedirectToAction("Deli", "Oder");
            }
            ViewBag.DeliveryStatuseId = new SelectList(await _deliveryStatuse.TabAsync(), "DeliveryStatuseId", "StatuseDelivery");
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Deliver(DeliveryModel model)
        {
            if(ModelState.IsValid)
            {
                var Co = await Product(User.Identity.Name);
                if (Co == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                else
                {
                  var de=await _delivery.GetBySupplIdAsync(Co.SupplierId);
                    if(de!=null)
                    {
                        ViewBag.Data = "You can only have have one Delivery details . you update the existing one";
                        return View(model);
                    }
                }
                model.SupplierId = Co.SupplierId;//

                await _delivery.AddAsync(model);
                return RedirectToAction("Deli", "Oder");
            }
            ViewBag.DeliveryStatuseId = new SelectList(await _deliveryStatuse.TabAsync(), "DeliveryStatuseId", "StatuseDelivery");
            return View(model);
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
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Deli()
        {
            var Co = await Product(User.Identity.Name);
            if (Co == null)
            {
                return RedirectToAction("Error", "Error");
            }
            var Model = await _delivery.TabBySupplierAsync(Co.SupplierId);

            ViewBag.DeliveryStatuseId = new SelectList(await _deliveryStatuse.TabAsync(), "DeliveryStatuseId", "StatuseDelivery");
            return View(Model);
        }
        [HttpGet]
        [Authorize(Roles = "Pro Plan,Free Plan,Premium Plan,Stand Plan")]
        public async Task<IActionResult> Deliver()
        {
            ViewBag.DeliveryStatuseId = new SelectList(await _deliveryStatuse.TabAsync(), "DeliveryStatuseId", "StatuseDelivery");
            return View();
        }
        public async Task<string> RefranceAsync()
        {
            const string letter = "A1B2C3D4E5F6G7H8I9JKLMNOPQRSTU";
            StringBuilder res = new StringBuilder();

            int z = 13;
            Random rndm = new Random();

            RefranceModel data = new RefranceModel();
            do
            {

                while (0 < z--)
                {
                    res.Append(letter[rndm.Next(letter.Length)]);
                }
                string dd = res.ToString();
                data = await _refrance.GetById(dd);
            } while (data != null);

            return res.ToString();
        }
        [HttpGet]
        public IActionResult eng(string returnUrl) //Deposite
        {
            Cart cart = GetCart();
            if (cart.Lines.Count() == 0)
            {
                return RedirectToAction("Index", "Cart", new { returnUrl = returnUrl });
            }
            return View();

        }
    }
}
