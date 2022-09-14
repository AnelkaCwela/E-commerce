using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class PaymentTypeModel
    {
        [Key]
        public Guid PaymentTypeId { get; set; }
        [Display(Name = "Payment Type")]
        [Required]

        public string PaymentType { get; set; }
    }
}
