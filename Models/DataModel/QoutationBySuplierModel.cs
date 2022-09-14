using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class QoutationBySuplierModel
    {
        [Key]

        public Guid QoutationStatuseSuplierId { get; set; }
        [Required]
        [Display(Name = "Qoutation Statuse Suplier")]
        public string QoutationStatuseSuplier { get; set; }
    }
}
