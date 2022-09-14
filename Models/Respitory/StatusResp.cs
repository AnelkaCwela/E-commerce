using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class StatusResp : IStatus
    {
        private readonly DBCONTEX context;
        public StatusResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<StatusModel> StatusTbl;

        public async Task<IEnumerable<StatusModel>> TabAsync()
        {
            return  await context.StatusTbl.ToListAsync();
        }

        public async Task<StatusModel> GetByIdAsync(Guid LikeId)
        {
            StatusModel Data = await context.StatusTbl.FirstOrDefaultAsync(x => x.StatusId == LikeId);
            return Data;
        }

        public  async Task<StatusModel> AddAsync(StatusModel _Like)
        {
           await context.StatusTbl.AddAsync(_Like);
            context.SaveChanges();
            return _Like;
        }

        public async Task<StatusModel> RemoveAsync(Guid LikeId)
        {
            StatusModel Data = await context.StatusTbl.FirstOrDefaultAsync(x => x.StatusId == LikeId);
            if (Data != null)
            {
                context.StatusTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<StatusModel> UpdaAsync(StatusModel _CategoryModel)
        {
            StatusModel Data = await context.StatusTbl.FirstOrDefaultAsync(x => x.StatusId == _CategoryModel.StatusId);
            if (Data != null)
            {
                Data.Statuse = _CategoryModel.Statuse;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.StatusTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
