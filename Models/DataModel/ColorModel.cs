using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace OurShop.Models.DataModel
{
    public class ColorModel
    {
        [Key]

        public Guid ColorId { get; set; }
        [Required(ErrorMessage = "Enter  Color")]
        [Display(Name = " Color")]
        public string Color { get; set; }
      
    }
}
