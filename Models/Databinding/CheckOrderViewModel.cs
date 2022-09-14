using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurShop.Models.Databinding
{
    public class CheckOrderViewModel
    {
        public int OrderId { get; set; }

        public StatusModel StatusModel { get; set; }

        public QuotationDetailModel quotationDetailModel { get; set; }
       

        public IteamModel IteamModel { get; set; }
        public QuotationModel quotationModel { get; set; }
        //public CustomerModel Customer { get; set; }
    }
}
