using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class QoutationStatuseModel
    {
        [Key]

        public Guid QoutationStatuseId { get; set; }
        [Required]
        [Display(Name = " Qoutation Statuse")]
        public string QoutationStatuse { get; set; }
    }
}
