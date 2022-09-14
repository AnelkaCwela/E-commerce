using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.DataModel
{
    public class SupplierTypeModel
    {
        [Key]
        public Guid SupplierTypeId { get; set; }

        [Required(ErrorMessage = "Enter Supplier Type")]
        [Display(Name = "Supplier Type")]
        public string SupplierType { get; set; }
    }
}
