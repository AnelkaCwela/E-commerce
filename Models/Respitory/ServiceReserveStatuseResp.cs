using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class ServiceReserveStatuseResp : IServiceReserveStatuse
    {
        private readonly DBCONTEX context;
        public ServiceReserveStatuseResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<ServiceReserveStatuseModel> ServiceReserveStatuseTbl;

        public  async Task<IQueryable<ServiceReserveStatuseModel>> TabAsync()
        {
            return (IQueryable<ServiceReserveStatuseModel>)await context.ServiceReserveStatuseTbl.ToListAsync();
        }

        public async Task<ServiceReserveStatuseModel> GetByIdAsync(Guid LikeId)
        {
            ServiceReserveStatuseModel Data = await context.ServiceReserveStatuseTbl.FirstOrDefaultAsync(x => x.ServiceReserveStatuseId == LikeId);
            return Data;
        }

        public async Task<ServiceReserveStatuseModel> AddAsync(ServiceReserveStatuseModel _Like)
        {
           await context.ServiceReserveStatuseTbl.AddAsync(_Like);
            await  context.SaveChangesAsync();
            return _Like;
        }

        public async Task<ServiceReserveStatuseModel> RemoveAsync(Guid LikeId)
        {
            ServiceReserveStatuseModel Data = await context.ServiceReserveStatuseTbl.FirstOrDefaultAsync(x => x.ServiceReserveStatuseId == LikeId);
            if (Data != null)
            {
                context.ServiceReserveStatuseTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<ServiceReserveStatuseModel> UpdaAsync(ServiceReserveStatuseModel _CategoryModel)
        {
            ServiceReserveStatuseModel Data = await context.ServiceReserveStatuseTbl.FirstOrDefaultAsync(x => x.ServiceReserveStatuseId == _CategoryModel.ServiceReserveStatuseId);
            if (Data != null)
            {
                Data.ServiceReserveStatuse = _CategoryModel.ServiceReserveStatuse;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.ServiceReserveStatuseTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
