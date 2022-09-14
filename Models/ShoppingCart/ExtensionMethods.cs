using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.ShoppingCart
{
    public static class ExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<IteamDetailModel> Iteams)
        {
            decimal total = 0;
            foreach (IteamDetailModel Item in Iteams)
            {
                total += 20;
                //total += Item?.Price ?? 0;
            }
            return total;
        }
    }
}
