using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
