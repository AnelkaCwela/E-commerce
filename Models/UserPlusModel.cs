using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OurShop.Models
{
    public class UserPlusModel : IdentityUser
    {
        public bool Bool { get; set; }
        public string Phone { get; set; }

    }
}
