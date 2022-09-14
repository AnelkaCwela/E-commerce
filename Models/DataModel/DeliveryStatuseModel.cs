using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurShop.Models.DataModel
{
    public class DeliveryStatuseModel
    {
        [Key]
        public Guid DeliveryStatuseId { get; set; }
        [Display(Name = "Delivery Statuse")]
        public string StatuseDelivery { get; set; }

    }
}
