using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class IteamDetailUpdateData
    {
        [Key]
        public Guid IteamDetailId { get; set; }
        public IEnumerable<ImageModel> images { get; set; }
        public IteamModel item { get; set; }
       


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
        public IEnumerable<IteamDetailModel> iteamDetailModels { get; set; }
    }
}
