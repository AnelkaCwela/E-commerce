using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OurShop.Models
{
    public class DBCONTEX : IdentityDbContext<UserPlusModel>
    {
        public DBCONTEX(DbContextOptions<DBCONTEX> options) : base(options)
        { }
        public DbSet<QuotationModel>Orders { get; set; }
        public DbSet<IteamDetailModel> IteamDetailTbl { get; set; }
        
        public DbSet<DeliveryStatuseModel> DeliveryStatuseTbl { get; set; }
        public DbSet<ReserveModel> ReserveTbl { get; set; }
        public DbSet<ServiceCategoryModel> ServiceCategoryTbl { get; set; }
        public DbSet<ServiceModel> ServiceTbl { get; set; }
        public DbSet<ServiceReserveStatuseModel> ServiceReserveStatuseTbl { get; set; }
        public DbSet<ServiceTypeModel> ServiceTypeTbl { get; set; }
        public DbSet<SeviceStauseModel> SeviceStauseTbl { get; set; }
        public DbSet<ContactDetailModel> ContactDetailTbl { get; set; }
        public DbSet<SupplierTypeModel> SupplierTypeTbl { get; set; }
        public DbSet<SupplierStatuseModel> SupplierStatuseTbl { get; set; }
        public DbSet<SububModel> SububTbl { get; set; }
        public DbSet<StatusModel> StatusTbl { get; set; }
        public DbSet<RatingModel> RatingTbl { get; set; }
        public DbSet<QoutationBySuplierModel> QoutationBySuplierTbl { get; set; }
        public DbSet<QoutationStatuseModel> QoutationStatuseTbl { get; set; }
        public DbSet<OderTypeModel> OderTypeTbl { get; set; }
        public DbSet<TypeModel> TypeModelTbl { get; set; }
        public DbSet<SupplierModel> SupplierModelTbl { get; set; }
        public DbSet<SizeModel> SizeModelTbl { get; set; }
        public DbSet<ResidentModel> ResidentModelTbl { get; set; }
        public DbSet<QuotationDetailModel> QuotationDetailModelTbl { get; set; }
        public DbSet<QuotationModel> QoutationModelTbl { get; set; }
       
        public DbSet<PaymentTypeModel> PaymentTypeModelTbl { get; set; }
        public DbSet<LocationModel> LocationModelTbl { get; set; }
        public DbSet<LikeModel> LikeModelTbl { get; set; }
        public DbSet<IteamStatuseModel> IteamStatuseTbl { get; set; }
        public DbSet<ImageModel> ImageModelTbl { get; set; }
        public DbSet<IteamModel> IteamModelTbl { get; set; }

        public DbSet<GanderModel> GanderModelTbl { get; set; }
        public DbSet<DeliveryModel> DeliveryModelTbl { get; set; }
        public DbSet<CustomerModel> CustomerModelTbl { get; set; }
        public DbSet<BrandDeatailModel> BrandDeatailTbl { get; set; }
        public DbSet<BrandModel> BrandModelTbl { get; set; }
        public DbSet<CategoryModel> CategoryModelTbl { get; set; }
        public DbSet<ColorModel> ColorModelTbl { get; set; }
        public DbSet<CityModel> CityModelTbl { get; set; }
        //BrandModelTbl
    }
}
