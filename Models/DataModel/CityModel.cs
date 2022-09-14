using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OurShop.Models.DataModel
{
    public class CityModel
    {
        [Key]

        public Guid CityId { get; set; }
        [Required(ErrorMessage = "Enter City")]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}
