using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class SizeModel
    {
        [Key]

        public Guid SizeId { get; set; }
        [Required(ErrorMessage = "Enter Size")]
        [Display(Name = "Size")]
        public string Size { get; set; }


    }
}
