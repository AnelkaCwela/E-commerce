using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.Databinding;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models
{
    public class EFOrderRepository /*: IOrderRepository*/
    {
        private DBCONTEX context;
        public EFOrderRepository(DBCONTEX ctx)
        {
            context = ctx;
        }
   
      //public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);
      //  public void SaveOrder(Order order)
      //  { 
      //      context.AttachRange(order.Lines.Select(l => l.Iteam));
      //      if (order.OrderID == 0)
      //      {
      //          context.Orders.Add(order);
      //      }
      //      context.SaveChanges();
      //  }
    }
}
