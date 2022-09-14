using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurShop.Models.DataModel
{
    public class IteamStatuseModel
    {
        [Key]
        public Guid IteamStatuseId { get; set; }

        [Required(ErrorMessage = "Enter Iteam Statuse ")]
        [Display(Name = "Iterm Statuse")]
        public string IteamStatuse { get; set; }
    }
}
