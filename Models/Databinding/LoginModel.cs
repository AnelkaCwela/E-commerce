using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class LoginModel
    {
        [Key]
        public int deleteId { get; set; }

        /// <summary>
        /// ////////////////////////
        /// </summary>
        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        //[Remote(action: "EmailIsInUsE", controller: "Account")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Display(Name = "Password")]
        public bool IsCustomer { get; set; }
    }
}
