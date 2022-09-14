using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class SeviceStauseModel
    {
        [Key]
        public Guid SeviceStauseId { get; set; }
        [Display(Name = "serviceStatuse")]
        public string SeviceStause { get; set; }
    }
}
