using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class SupplierModel
    {
        [Key]
        public Guid SupplierId { get; set; }

        public string SupplierUserName { get; set; }

        public DateTime RegistartionDate { get; set; }
        public Guid SupplierTypeId { get; set; }
        [ForeignKey("SupplierTypeId")]
        public SupplierTypeModel SupplierTypeModel { get; set; }

        public Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public BrandModel BrandModel { get; set; }


        public Guid SupplierStatuseId { get; set; }
        [ForeignKey("SupplierStatuseId")]
        public SupplierStatuseModel SupplierStatuseModel { get; set; }
        public Guid BrandDeatailId { get; set; }
        [ForeignKey("BrandDeatailId")]
        public BrandDeatailModel BrandDeatailModel { get; set; }
    }
}
