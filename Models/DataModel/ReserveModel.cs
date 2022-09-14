using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class ReserveModel
    {
        [Key]
        public Guid ReserveId { get; set; }
        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Display(Name = "Phone")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date")]
        [Required]
        public DateTime ReserveDate { get; set; }
        [ForeignKey("ServiceReserveStatuseId")]
        public Guid ServiceReserveStatuseId { get; set; }
        public ServiceReserveStatuseModel ServiceReserveStatuseModel { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public ServiceModel ServiceModel { get; set; }

    }
}
