using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace OurShop.Models.Databinding
{
    public class RoleModel
    {

        [Key]
        public int RoleId
        {
            get; set;
        }
        public string RoleName
        {
            get
; set;
        }
    }
}
