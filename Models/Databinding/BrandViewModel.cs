using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OurShop.Models.DataModel;

namespace OurShop.Models.Databinding
{
    public class BrandViewModel
    {
        [Key]
        public int ContactDetailId { get; set; }
        [Required(ErrorMessage = "Enter TelNo")]
        [Display(Name = "TelNo")]
        public string TelNo { get; set; }




        [Display(Name = "WhastpLink")]
        public string whastpLink { get; set; }





        [Required(ErrorMessage = "EmailAddress")]
        [Display(Name = "EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }



     
        [Required(ErrorMessage = "Brand Name")]

        [Display(Name = "Brand Name / Company Name")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Enter Brand Slogn ")]
        [Display(Name = "Brand Slogn ")]

        public byte[] BrandLogo { get; set; }
    }
}
