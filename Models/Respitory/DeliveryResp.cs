using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class DeliveryResp : IDelivery
    {
        private readonly DBCONTEX context;
        public DeliveryResp(DBCONTEX _context)
        {
            context = _context;
        }

        public Task<IQueryable<DeliveryModel>> DeliveryModelTbl;

        public async Task<IQueryable<DeliveryModel>> TabAsync()
        {
            return (IQueryable<DeliveryModel>)await context.DeliveryModelTbl.ToListAsync();
        }

        public async Task<DeliveryModel> GetByIdAsync(Guid DeliveryId)
        {
            return await context.DeliveryModelTbl.FirstOrDefaultAsync(x => x.DeliveryId == DeliveryId);
          
        }

        public async Task<DeliveryModel> AddAsync(DeliveryModel _Delivery)
        {
            await context.DeliveryModelTbl.AddAsync(_Delivery);
           await context.SaveChangesAsync();
            return _Delivery;
        }

        public async Task<DeliveryModel> RemoveAsync(Guid DeliveryId)
        {
            DeliveryModel Data = await context.DeliveryModelTbl.FirstOrDefaultAsync(x => x.DeliveryId == DeliveryId);
            if (Data != null)
            {
                context.DeliveryModelTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<DeliveryModel> UpdatAsync(DeliveryModel DeliveryId)
        {
            DeliveryModel Data = await context.DeliveryModelTbl.FirstOrDefaultAsync(x => x.DeliveryId == DeliveryId.DeliveryId);
            if (Data != null)
            {
                Data.deliverydate = DeliveryId.deliverydate;
                Data.QoutationId = DeliveryId.QoutationId;
                Data.DeliveryStatuseId = DeliveryId.DeliveryStatuseId;

                var save = context.DeliveryModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
