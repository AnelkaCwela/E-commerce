using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurShop.Models.DataModel
{
    public class BrandModel
    {
        [Key]

        public Guid BrandId { get; set; }
        [Required(ErrorMessage = "Brand Name")]
        [MaxLength(10)]
        [Display(Name = "Brand Name / Company Name")]

        public string BrandName { get; set; }
      
        [Required(ErrorMessage = "Enter Brand Slogn ")]
        [Display(Name = "Brand Slogn ")]
        [MaxLength(20)]
        public string BrandSlogn { get; set; }
    
        [Display(Name = "Brand Logo")]
        [DataType(DataType.Upload)]
      
        public byte[] BrandLogo { get; set; }
    }
}
