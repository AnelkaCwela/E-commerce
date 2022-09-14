using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class IteamDetaiInsertModel
    {
        [Key]
        public Guid IteamDetailId { get; set; }

        public bool Stock { get; set; } = false;

        [Required(ErrorMessage = "No of Iteam")]
        [Display(Name = "No of Iteam")]
        public int NoOfIteam { get; set; }


        [Display(Name = "Size")]
        [Required]
        public Guid SizeId { get; set; }
        [ForeignKey("SizeId")]
        public SizeModel SizeModel { get; set; }


        [Display(Name = "Color")]
        [Required]
        public Guid ColorId { get; set; }
        [ForeignKey("ColorId")]
        public ColorModel ColorModel { get; set; }

        public Guid IteamId { get; set; }
        [ForeignKey("IteamId")]
        public IteamModel IteamModel { get; set; }



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
        [Display(Name = "Product Image")]
        public byte[] ImageData { get; set; }
    }
}
