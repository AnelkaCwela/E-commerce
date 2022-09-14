using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class ForgotPasswordModel
    {
        [Key]
        public int deleteId { get; set; }
        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
    }
}
