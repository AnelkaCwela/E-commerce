using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class ImageModel
    {
        [Key]
        public Guid ImageId { get; set; }
        [Required(ErrorMessage = "Photo Requierd")]

        [Display(Name = "Photo")]
        public byte[] ImageData { get; set; }
        public Guid IteamId { get; set; }
        [ForeignKey("IteamId")]
        public IteamModel IteamModel { get; set; }
    }
}
