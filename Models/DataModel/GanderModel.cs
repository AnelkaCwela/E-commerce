using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurShop.Models.DataModel
{
    public class GanderModel
    {
        [Key]
        public Guid GanderId { get; set; }
        [Required]
        [Display(Name = "Gander")]
        public string Gander { get; set; }
        
    }
}
