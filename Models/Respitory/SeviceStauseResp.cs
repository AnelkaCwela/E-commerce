using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class SeviceStauseResp : ISeviceStause
    {
        private readonly DBCONTEX context;
        public SeviceStauseResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<SeviceStauseModel> SeviceStauseTbl;

        public async Task<IQueryable<SeviceStauseModel>> TabAsync()
        {
            return (IQueryable<SeviceStauseModel>)await context.SeviceStauseTbl.ToListAsync();
        }

        public async Task<SeviceStauseModel> GetByIdAsync(Guid LikeId)
        {
            SeviceStauseModel Data = await context.SeviceStauseTbl.FirstOrDefaultAsync(x => x.SeviceStauseId == LikeId);
            return Data;
        }

        public async Task<SeviceStauseModel> AddAsync(SeviceStauseModel _Like)
        {
           await context.SeviceStauseTbl.AddAsync(_Like);
            await   context.SaveChangesAsync();
            return _Like;
        }

        public async Task<SeviceStauseModel> RemoveAsync(Guid LikeId)
        {
            SeviceStauseModel Data = await context.SeviceStauseTbl.FirstOrDefaultAsync(x => x.SeviceStauseId == LikeId);
            if (Data != null)
            {
                context.SeviceStauseTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<SeviceStauseModel> UpdaAsync(SeviceStauseModel _CategoryModel)
        {
            SeviceStauseModel Data = await context.SeviceStauseTbl.FirstOrDefaultAsync(x => x.SeviceStauseId == _CategoryModel.SeviceStauseId);
            if (Data != null)
            {
                Data.SeviceStause = _CategoryModel.SeviceStause;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.SeviceStauseTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }

    }
}
