using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class ReserveResp : IReserve
    {
        private readonly DBCONTEX context;
        public ReserveResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<ReserveModel> ReserveTbl;

        public async Task<IQueryable<ReserveModel>> TabAsync()
        {
            return (IQueryable<ReserveModel>)await context.ReserveTbl.ToListAsync();
        }

        public async Task<ReserveModel> GetByIdAsync(Guid LikeId)
        {
            ReserveModel Data = await context.ReserveTbl.FirstOrDefaultAsync(x => x.ReserveId == LikeId);
            return Data;
        }

        public async Task<ReserveModel> AddAsync(ReserveModel _Like)
        {
           await context.ReserveTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public  async Task<ReserveModel> RemoveAsync(Guid LikeId)
        {
            ReserveModel Data = await context.ReserveTbl.FirstOrDefaultAsync(x => x.ReserveId == LikeId);
            if (Data != null)
            {
                context.ReserveTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<ReserveModel> UpdaAsync(ReserveModel _CategoryModel)
        {
            ReserveModel Data = await context.ReserveTbl.FirstOrDefaultAsync(x => x.ReserveId == _CategoryModel.ReserveId);
            if (Data != null)
            {
                Data.ReserveDate = _CategoryModel.ReserveDate;
                Data.ServiceReserveStatuseId = _CategoryModel.ServiceReserveStatuseId;

                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.ReserveTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
