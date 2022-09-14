using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OurShop.Models.DataModel;

namespace OurShop.Models.Databinding
{
    public class IteamDatamodel
    {
        [Key]

        public Guid Id { get; set; }


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


        [Display(Name = "Product")]
        public byte[] ImageData { get; set; }

    }
}
