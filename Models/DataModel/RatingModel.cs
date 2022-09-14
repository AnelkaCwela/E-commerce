using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class RatingModel
    {
        [Key]
        public Guid RatingId { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel CustomerModel { get; set; }
        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public SupplierModel supplierModel { get; set; }
    }
}
