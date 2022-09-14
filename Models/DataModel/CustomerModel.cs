using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class CustomerModel
    {
        [Key]
        public Guid CustomerId { get; set; }

        public string CustomerUserName { get; set; }
       
    }
}
