using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurShop.Models.DataModel
{
    public class TypeModel
    {
        [Key]

        public Guid TypeId { get; set; }
        [Required(ErrorMessage = "Enter Type Name")]
        [Display(Name = "Type")]
        public string TypeName { get; set; }
      
    }
}
