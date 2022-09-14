using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OurShop.Models.DataModel
{
    public class ServiceModel
    {
        [Key]
        public Guid ServiceId { get; set; }
        [Display(Name = "Price")]
        [Required]
        public double Price { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string desctription { get; set; }
        [Display(Name = "Photo")]
        [Required]
        public byte[] ServicePhoto { get; set; }
        public DateTime ServiceDate { get; set; }
        public Guid SeviceStauseId { get; set; }
        [ForeignKey("SeviceStauseId")]

        public SeviceStauseModel SeviceStauseModel { get; set; }
        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public SupplierModel SupplierModel { get; set; }
        public Guid ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public ServiceTypeModel ServiceTypeModel { get; set; }
    }

}
