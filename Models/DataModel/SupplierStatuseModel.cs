using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class SupplierStatuseModel
    {
        [Key]
        public Guid SupplierStatuseId { get; set; }
        [Required(ErrorMessage = "Enter Supplier Statuse")]
        [Display(Name = "Supplier Statuse")]
        public string SupplierStatuse { get; set; }
    }
}
