using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class ServiceResp : IService
    {
        private readonly DBCONTEX context;
        public ServiceResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<ServiceModel> ServiceTbl;

        public async Task<IQueryable<ServiceModel>> TabAsync()
        {
            return (IQueryable<ServiceModel>)await context.ServiceTbl.ToListAsync();
        }

        public async Task<ServiceModel> GetByIdAsync(Guid LikeId)
        {
            ServiceModel Data = await context.ServiceTbl.FirstOrDefaultAsync(x => x.ServiceId == LikeId);
            return Data;
        }

        public async Task<ServiceModel> AddAsync(ServiceModel _Like)
        {
           await context.ServiceTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<ServiceModel> RemoveAsync(Guid LikeId)
        {
            ServiceModel Data = await context.ServiceTbl.FirstOrDefaultAsync(x => x.ServiceId == LikeId);
            if (Data != null)
            {
                context.ServiceTbl.Remove(Data);
              await  context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<ServiceModel> UpdaAsync(ServiceModel _CategoryModel)
        {
            ServiceModel Data = await context.ServiceTbl.FirstOrDefaultAsync(x => x.ServiceId == _CategoryModel.ServiceId);
            if (Data != null)
            {
                Data.desctription = _CategoryModel.desctription;
                Data.Price = _CategoryModel.Price;
                Data.ServiceDate = _CategoryModel.ServiceDate;
                Data.ServicePhoto = _CategoryModel.ServicePhoto;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.ServiceTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
