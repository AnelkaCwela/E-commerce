using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class IteamModel
    {
        [Key]

        public Guid IteamId { get; set; }

        [Required(ErrorMessage = "Enter Iterm Name ")]
        [Display(Name = "Iterm Name")]
        public string ItermName { get; set; }


        [Display(Name = "Date")]
        public DateTime ItermDate { get; set; }

        
        [Required(ErrorMessage = "Enter Price")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


        [Display(Name = "DiscountPrice")]
        [DataType(DataType.Currency)]
        public decimal DiscountPrice { get; set; } = 0;

        
        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public SupplierModel SupplierModel { get; set; }


        [Display(Name = "Category")]
        [Required]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryModel CategoryModel { get; set; }


        [Display(Name = "Gander")]
        [Required]
        public Guid GanderId { get; set; }
        [ForeignKey("GanderId")]
        public GanderModel GanderModel { get; set; }

        public bool InStock = false;
       
        

        [Display(Name = "Photo")]
        public byte[] ImageIteam { get; set; }

        public Guid IteamStatuseId { get; set; }
        [ForeignKey("IteamStatuseId")]
        public IteamStatuseModel IteamStatuseModel { get; set; }
    }
}
