using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OurShop.Models.Databinding
{
    public class RegisterUserModel
    {

        [Key]
        public int Id { get; set; }
        //[Display(Name = "Confirmation Code")]
        //[Required]
        //public string Code { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
       [Remote(action: "EmailIsInUsE", controller: "Account")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string ConfirmPassword { get; set; }
       // public bool Is { get; set; } = true;
    }
}
