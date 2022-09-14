using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using OurShop.Models.DataModel;
namespace OurShop.Models.ShoppingCart
{
    public class ShoppingCartModel : IEnumerable<IteamDetailModel>
    {
        public IEnumerable<IteamDetailModel> Iteams { get; set; }
        public IEnumerator<IteamDetailModel> GetEnumerator()
        {
            return Iteams.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
