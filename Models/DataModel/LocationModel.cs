using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class LocationModel
    {
        [Key]
        public Guid LocationId { get; set; }

        public string Address { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }

        public int Zoom { get; set; }
        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public SupplierModel supplierModel { get; set; }
    }
}
