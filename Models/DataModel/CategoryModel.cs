using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class CategoryModel
    {
        [Key]

        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Enter Category Name")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
       
        public Guid TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TypeModel TypeModel { get; set; }
    }
}
