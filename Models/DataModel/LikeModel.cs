using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class LikeModel
    {
        [Key]

        public Guid LikeId { get; set; }


        public bool Like { get; set; } = false;

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }
        public Guid IteamId { get; set; }
        [ForeignKey("IteamId")]
        public IteamModel IteamModel { get; set; }
    }
}
