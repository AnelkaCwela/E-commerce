using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class LikeResp : ILike
    {
        private readonly DBCONTEX context;
        public LikeResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<LikeModel> LikeModelTbl;

        public async Task<IQueryable<LikeModel>> TabAsync()
        {
            return (IQueryable<LikeModel>) await context.LikeModelTbl.ToListAsync();
        }

        public async Task<LikeModel> GetByIdAsync(Guid LikeId)
        {
            return  await context.LikeModelTbl.FirstOrDefaultAsync(x => x.LikeId == LikeId);
           
        }

        public async Task<LikeModel> AddAsync(LikeModel _Like)
        {
            await context.LikeModelTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<LikeModel> RemoveAsync(Guid LikeId)
        {
            LikeModel Data = await context.LikeModelTbl.FirstOrDefaultAsync(x => x.LikeId == LikeId);

            if (Data != null)
            {
                context.LikeModelTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;

        }
    }
}
