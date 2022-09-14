using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Databinding
{
    public class IteamViewModel
    {
        [Key]
        public int Id { get; set; }
       
        public IteamModel IteamModel { get; set; }
          public IteamStatuseModel Statuse { get; set; }
        public CategoryModel CategoryModel { get; set; }
        public GanderModel GanderModel { get; set; }   

    }
}
