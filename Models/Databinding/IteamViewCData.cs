using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class IteamViewCData
    {
        public Guid id { get; set; }
        public Guid ColorId { get; set; }
        public Guid SizeId { get; set; }
        public int No { get; set; }
      public  string returnUrl { get; set; }
        public IEnumerable<ImageModel> images { get; set; }
        public IteamModel item { get; set; }
        public IEnumerable<IteamModel> IteamModel { get; set; }
        public IEnumerable<IteamDetailModel> iteamDetailModels { get; set; }
    }
}
