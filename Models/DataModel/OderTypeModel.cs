using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace OurShop.Models.DataModel
{
    public class OderTypeModel
    {
        [Key]
        public Guid OderTypeId { get; set; }
        [Display(Name = "OderType")]
        [Required]
        public string OderType { get; set; }//Take ,drop,PickUp
    }
}
