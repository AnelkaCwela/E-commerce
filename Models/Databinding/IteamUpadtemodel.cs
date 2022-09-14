using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class IteamUpadtemodel
    {
        [Key]

        public Guid Id { get; set; }

        [Display(Name = "DiscountPrice")]
        [DataType(DataType.Currency)]
        public decimal DiscountPrice { get; set; } = 0;

        [Required(ErrorMessage = "Enter Iterm Name ")]
        [Display(Name = "Iterm Name")]
        [MaxLength(10)]
        public string ItermName { get; set; }


        [Required(ErrorMessage = "Enter Price")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        [Required]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryModel CategoryModel { get; set; }


        [Display(Name = "Gander")]
        public Guid GanderId { get; set; }
        [ForeignKey("GanderId")]
        public GanderModel GanderModel { get; set; }



        [Display(Name = "Product type")]
        [Required]
        public Guid TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TypeModel TypeModel { get; set; }
    }
}
