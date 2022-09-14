using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class IteamViewData
    {
        public IEnumerable<ImageModel> images { get; set; }
        public IteamModel item { get; set; }
        public IEnumerable<IteamDetailModel> iteamDetailModels { get; set; }
    }
}
