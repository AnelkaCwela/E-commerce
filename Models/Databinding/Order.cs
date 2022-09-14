using OurShop.Models.DataModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }




        [Display(Name = "Date Time ")]
        public DateTime QoutationDate { get; set; }
        [Display(Name = "Refrance No")]
        public string RefranceNo { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public StatusModel StatusModel { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }

        public int SubrbId { get; set; }
        [ForeignKey("SubrbId")]
        public SububModel SububModel { get; set; }
        public int OderTypeId { get; set; }
        [ForeignKey("OderTypeId")]
        public OderTypeModel OderTypeModel { get; set; }
        public int PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public PaymentTypeModel PaymentTypeModel { get; set; }
        public int QoutationStatuseId { get; set; }
        [ForeignKey("QoutationStatuseId")]
        public QoutationStatuseModel QoutationStatuseModel { get; set; }
        public int QoutationStatuseSuplierId { get; set; }
        [ForeignKey("QoutationStatuseSuplierId")]
        public QoutationBySuplierModel QoutationBySuplierModel { get; set; }

    }
}
