using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Databinding
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(IteamDetailModel iteam, int quantity,Guid ColorId ,Guid SizeId,decimal Price)
        {
            CartLine line = lineCollection
            .Where(p => p.Iteam.IteamDetailId == iteam.IteamDetailId)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Iteam = iteam,
                    Quantity = quantity,
                    ColorId= ColorId
                    ,
                    SizeId= SizeId,Price= Price

                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        //public virtual void UpadteItem(IteamModel iteam)
        //{
        //    CartLine line = lineCollection
        //    .Where(p => p.Iteam.IteamId == iteam.IteamId)
        //    .FirstOrDefault();
           
        //    if(line!=null)
        //    {
        //        line.Quantity = iteam.NoOfIteam;
        //        line.ColorId = iteam.ColorId;
        //        line.SizeId = iteam.SizeId;
        //        line.GanderId = iteam.GanderId;
        //    }
        //}
        public virtual void RemoveLine(IteamDetailModel product) =>
        lineCollection.RemoveAll(l => l.Iteam.IteamDetailId == product.IteamDetailId);
        public virtual decimal ComputeTotalValue() =>
        lineCollection.Sum(e => e.Price*e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public Guid Id { get; set; }
        //public int QoutationId { get; set; }
        public Guid ColorId { get; set; }
        public decimal Price { get; set; }
        public Guid SizeId { get; set; }
        public IteamDetailModel Iteam { get; set; }
        public int Quantity { get; set; }
    }
}
