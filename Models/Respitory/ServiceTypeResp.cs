using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class ServiceTypeResp : IServiceType
    {
        private readonly DBCONTEX context;
        public ServiceTypeResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<ServiceTypeModel> ServiceTypeTbl;

        public async Task<IQueryable<ServiceTypeModel>> TabAsync()
        {
            return (IQueryable<ServiceTypeModel>)await context.ServiceTypeTbl.ToListAsync();
        }

        public async Task<ServiceTypeModel> GetByIdAsync(Guid LikeId)
        {
            ServiceTypeModel Data = await context.ServiceTypeTbl.FirstOrDefaultAsync(x => x.ServiceTypeId == LikeId);
            return Data;
        }

        public async Task<ServiceTypeModel> AddAsync(ServiceTypeModel _Like)
        {
          await  context.ServiceTypeTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public  async Task<ServiceTypeModel> RemoveAsync(Guid LikeId)
        {
            ServiceTypeModel Data = await context.ServiceTypeTbl.FirstOrDefaultAsync(x => x.ServiceTypeId == LikeId);
            if (Data != null)
            {
                context.ServiceTypeTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<ServiceTypeModel> UpdaAsync(ServiceTypeModel _CategoryModel)
        {
            ServiceTypeModel Data = await context.ServiceTypeTbl.FirstOrDefaultAsync(x => x.ServiceTypeId == _CategoryModel.ServiceTypeId);
            if (Data != null)
            {
                Data.ServiceType = _CategoryModel.ServiceType;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.ServiceTypeTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
