using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class StatusModel
    {
        [Key]
        public Guid StatusId { get; set; }
        [Required(ErrorMessage = "Enter  Statuse")]
        [Display(Name = "Statuse")]
        public string Statuse { get; set; }
        //public List<QuotationModel> QoutationModelS { get; set; }
    }
}
