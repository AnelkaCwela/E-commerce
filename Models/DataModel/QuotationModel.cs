using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class QuotationModel
    {

        [Key]
        public Guid QoutationId { get; set; }

        [Display(Name = "Date Time ")]
        public DateTime QoutationDate { get; set; }
        [Display(Name = "Refrance No")]
        public string RefranceNo { get; set; }

        [Display(Name = "Total Price")]
        
        public double TotalQoutationPrice { get; set; }
        [Display(Name = "Refrance")]
        public string Refrance { get; set; }

        //public Guid StatusId { get; set; }
        //[ForeignKey("StatusId")]
        //public StatusModel StatusModel { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }
        
        public Guid SubrbId { get; set; }
        [ForeignKey("SubrbId")]
        public SububModel SububModel { get; set; }
        public Guid OderTypeId { get; set; }
        [ForeignKey("OderTypeId")]
        public OderTypeModel OderTypeModel { get; set; }
        //public Guid PaymentTypeId { get; set; }
        //[ForeignKey("PaymentTypeId")]
        //public PaymentTypeModel PaymentTypeModel { get; set; }
        public Guid QoutationStatuseId { get; set; }
        [ForeignKey("QoutationStatuseId")]
        public QoutationStatuseModel QoutationStatuseModel { get; set; }
        public Guid QoutationStatuseSuplierId { get; set; }
        [ForeignKey("QoutationStatuseSuplierId")]
        public QoutationBySuplierModel QoutationBySuplierModel { get; set; }
    }
}
