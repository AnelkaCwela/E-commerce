using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class ServiceCategoryResp : IServiceCategory
    {
        private readonly DBCONTEX context;
        public ServiceCategoryResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<ServiceCategoryModel> ServiceCategoryTbl;

        public async Task<IQueryable<ServiceCategoryModel>> TabAsync()
        {
            return (IQueryable<ServiceCategoryModel>) await context.ServiceCategoryTbl.ToListAsync();
        }

        public async Task<ServiceCategoryModel> GetByIdAsync(Guid LikeId)
        {
            ServiceCategoryModel Data = await context.ServiceCategoryTbl.FirstOrDefaultAsync(x => x.ServiceCategoryId == LikeId);
            return Data;
        }

        public async Task<ServiceCategoryModel> AddAsync(ServiceCategoryModel _Like)
        {
           await context.ServiceCategoryTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<ServiceCategoryModel> RemoveAsync(Guid LikeId)
        {
            ServiceCategoryModel Data = await context.ServiceCategoryTbl.FirstOrDefaultAsync(x => x.ServiceCategoryId == LikeId);
            if (Data != null)
            {
                context.ServiceCategoryTbl.Remove(Data);
                await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<ServiceCategoryModel> UpdaAsync(ServiceCategoryModel _CategoryModel)
        {
            ServiceCategoryModel Data = await context.ServiceCategoryTbl.FirstOrDefaultAsync(x => x.ServiceCategoryId == _CategoryModel.ServiceCategoryId);
            if (Data != null)
            {
                Data.ServiceCategory = _CategoryModel.ServiceCategory;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.ServiceCategoryTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
