using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OurShop.Models.DataModel
{
    public class ServiceCategoryModel
    {
        [Key]
        public Guid ServiceCategoryId { get; set; }
        [Display(Name = "Category")]
        [Required]
        public string ServiceCategory { get; set; }
    }
}
