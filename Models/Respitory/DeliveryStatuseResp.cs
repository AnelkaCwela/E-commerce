using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class DeliveryStatuseResp : IDeliveryStatuse
    {
        private readonly DBCONTEX context;
        public DeliveryStatuseResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<DeliveryStatuseModel> DeliveryStatuseTbl;

        public async Task<IQueryable<DeliveryStatuseModel>> TabAsync()
        {
            return (IQueryable<DeliveryStatuseModel>)await context.DeliveryStatuseTbl.ToListAsync();
        }

        public async Task<DeliveryStatuseModel> GetByIdAsync(Guid LikeId)
        {
           return await context.DeliveryStatuseTbl.FirstOrDefaultAsync(x => x.DeliveryStatuseId == LikeId);
            
        }

        public async Task<DeliveryStatuseModel> AddAsync(DeliveryStatuseModel _Like)
        {
           await context.DeliveryStatuseTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<DeliveryStatuseModel> RemoveAsync(Guid LikeId)
        {
            DeliveryStatuseModel Data = await context.DeliveryStatuseTbl.FirstOrDefaultAsync(x => x.DeliveryStatuseId == LikeId);
            if (Data != null)
            {
                context.DeliveryStatuseTbl.Remove(Data);
                await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<DeliveryStatuseModel> UpdaAsync(DeliveryStatuseModel _CategoryModel)
        {
            DeliveryStatuseModel Data = await context.DeliveryStatuseTbl.FirstOrDefaultAsync(x => x.DeliveryStatuseId == _CategoryModel.DeliveryStatuseId);
            if (Data != null)
            {
                Data.StatuseDelivery = _CategoryModel.StatuseDelivery;
                //Data.Address2 = _CategoryModel.Address2;
                //Data.CityId = _CategoryModel.CityId;
                //Data.ProvinceId = _CategoryModel.ProvinceId;
                //Data.ZipCode = _CategoryModel.ZipCode;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.DeliveryStatuseTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
