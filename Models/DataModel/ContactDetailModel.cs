using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class ContactDetailModel
    {
        [Key]
        public Guid ContactDetailId { get; set; }
        [Required(ErrorMessage = "Enter TelNo")]
        [Display(Name = "TelNo")]
        [DataType(DataType.PhoneNumber)]
        public string TelNo { get; set; }
        [Display(Name = "FaxNo")]

        [DataType(DataType.PhoneNumber)]
        public string FaxNo { get; set; } = null;

        [Required(ErrorMessage = "EmailAddress")]
        [Display(Name = "Bussness EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public BrandModel SupplierModel { get; set; }
    }
}
