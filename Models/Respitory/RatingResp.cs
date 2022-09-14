using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class RatingResp : IRating
    {
        private readonly DBCONTEX context;
        public RatingResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<RatingModel> RatingTbl;

        public async Task<IQueryable<RatingModel>> TabAsync()
        {
            return (IQueryable<RatingModel>)await context.RatingTbl.ToListAsync();
        }

        public async Task<RatingModel> GetByIdAsync(Guid LikeId)
        {
            RatingModel Data = await context.RatingTbl.FirstOrDefaultAsync(x => x.RatingId == LikeId);
            return Data;
        }

        public async Task<RatingModel> AddAsync(RatingModel _Like)
        {
           await context.RatingTbl.AddAsync(_Like);
            await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<RatingModel> RemoveAsync(Guid LikeId)
        {
            RatingModel Data = await context.RatingTbl.FirstOrDefaultAsync(x => x.RatingId == LikeId);
            if (Data != null)
            {
                context.RatingTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<RatingModel> UpdaAsync(RatingModel _CategoryModel)
        {
            RatingModel Data = await context.RatingTbl.FirstOrDefaultAsync(x => x.RatingId == _CategoryModel.RatingId);
            if (Data != null)
            {
                Data.CustomerId = _CategoryModel.CustomerId;
                Data.Rating = _CategoryModel.Rating;


                Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.RatingTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }

    }
}
