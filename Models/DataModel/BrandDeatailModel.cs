using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurShop.Models.DataModel
{
    public class BrandDeatailModel
    {
        [Key]
        public Guid BrandDeatailId { get; set; }
        [Display(Name = "Bussines Name / Brand Name")]
        [Required]
        public string BussneName { get; set; }
        [Required]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Identity Document, proof of Bussness Registration ")]
       
        public byte[] IdentityDocument { get; set; }
        [Display(Name = "Bussness Registration")]
        public byte[] BussnessRegistration { get; set; }


    }
}
