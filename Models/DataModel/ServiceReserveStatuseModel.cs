using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class ServiceReserveStatuseModel
    {
        [Key]
        public Guid ServiceReserveStatuseId { get; set; }
        [Required]
        public string ServiceReserveStatuse { get; set; }

    }
}
