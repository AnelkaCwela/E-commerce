using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class SububResp : ISubub
    {
        private readonly DBCONTEX context;
        public SububResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<SububModel> SububTbl;

        public async Task<IQueryable<SububModel>> TabAsync()
        {
            return (IQueryable<SububModel>)await context.SububTbl.ToListAsync();
        }

        public async Task<SububModel> GetByIdAsync(Guid LikeId)
        {
            SububModel Data = await context.SububTbl.FirstOrDefaultAsync(x => x.SubrbId == LikeId);
            return Data;
        }
        

        public async Task<SububModel> AddAsync(SububModel _Like)
        {
           await context.SububTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<SububModel> RemoveAsync(Guid LikeId)
        {
            SububModel Data = await context.SububTbl.FirstOrDefaultAsync(x => x.SubrbId == LikeId);
            if (Data != null)
            {
                context.SububTbl.Remove(Data);
              await  context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<SububModel> UpdaAsync(SububModel _CategoryModel)
        {
            SububModel Data = await context.SububTbl.FirstOrDefaultAsync(x =>x.SubrbId == _CategoryModel.SubrbId);
            if (Data != null)
            {
                Data.Address1 = _CategoryModel.Address1;
                Data.Address2 = _CategoryModel.Address2;
                Data.CityId = _CategoryModel.CityId;
                //Data.ProvinceId = _CategoryModel.ProvinceId;
                Data.PostCode = _CategoryModel.PostCode;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.SububTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
