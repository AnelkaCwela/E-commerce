using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.DataModel
{
    public class QuotationDetailModel
    {
        [Key]
        public Guid QoutationDetailId { get; set; }


        [Display(Name = "No Of Iteam ")]
        public int Quantty { get; set; }

        
        public Guid QoutationId { get; set; }

        [ForeignKey("QoutationId")]
        public QuotationModel QuotationModel { get; set; }
        public Guid IteamDetailId { get; set; }
        [ForeignKey("IteamDetailId")]
        public IteamDetailModel IteamModel { get; set; }



    }
}
