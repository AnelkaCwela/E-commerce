using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class IteamDetailModel
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
    }
}
