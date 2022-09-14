using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class ResidentModel
    {
        [Key]
        public Guid ResidentId
        {
            get; set;
        }


        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address Name")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Enter Street Address")]
        [Display(Name = "Street Line2  Address")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Enter Suburb")]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "Enter PostCode")]
        [Display(Name = "PostCode")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public SupplierModel SupplierModel { get; set; }
        [Required]
        public Guid CityId { get; set; }
        [ForeignKey("CityId")]
        public CityModel CityModel { get; set; }

    }
}
