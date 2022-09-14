using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OurShop.Models.DataModel
{
    public class ServiceTypeModel
    {
        [Key]
        public Guid ServiceTypeId { get; set; }
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
        public Guid ServiceCategoryId { get; set; }
        [ForeignKey("ServiceCategoryId")]
        public ServiceCategoryModel ServiceCategoryModel { get; set; }
    }
}
