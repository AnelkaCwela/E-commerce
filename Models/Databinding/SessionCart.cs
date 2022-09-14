using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OurShop.Models.DataModel;
namespace OurShop.Models.Databinding
{
    public class SessionCart : Cart 
    {
    
           
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
            ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(IteamDetailModel iteam, int quantity, Guid ColorId, Guid SizeId,decimal Price)
        {
            base.AddItem(iteam, quantity, ColorId, SizeId, Price);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(IteamDetailModel product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
