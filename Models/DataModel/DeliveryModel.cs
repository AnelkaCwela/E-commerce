using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class DeliveryModel
    {
        [Key]
        public Guid DeliveryId { get; set; }

        [Required(ErrorMessage = "Enter Delivery Date")]
        [Display(Name = "Delivery Date")]
        public DateTime deliverydate { get; set; }
        
        public Guid QoutationId { get; set; }

        [ForeignKey("QoutationId")]
        public QuotationModel QuotationModel { get; set; }
        public Guid DeliveryStatuseId { get; set; }

        [ForeignKey("DeliveryStatuseId")]
        public DeliveryStatuseModel DeliveryStatuse { get; set; }
    }
}
