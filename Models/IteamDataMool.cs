using OurShop.Models.Databinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models
{
    public class IteamDataMool
    {
        [Key]

        public int IteamId { get; set; }

        public IteamViewModel iteamViewModel { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
