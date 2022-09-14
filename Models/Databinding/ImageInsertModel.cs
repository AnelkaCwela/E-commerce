using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class ImageInsertModel
    {

        [Key]
        public Guid ImageId { get; set; }
        [Required(ErrorMessage = "Photo Requierd")]

        [Display(Name = "Photo")]
        public byte[] ImageData { get; set; }
        public Guid IteamId { get; set; }
        [ForeignKey("IteamId")]
        public IteamModel IteamModel { get; set; }

        [Display(Name = "Iterm Name")]
        [MaxLength(10)]
        public string ItermName { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Gander")]
        public Guid GanderId { get; set; }
        [ForeignKey("GanderId")]
        public GanderModel GanderModel { get; set; }
        [Display(Name = "Category")]
        [Required]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryModel CategoryModel { get; set; }

    }
}
