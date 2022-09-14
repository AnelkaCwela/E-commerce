using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class SizeResp : ISize
    {
        private readonly DBCONTEX context;
        public SizeResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<SizeModel> SizeModelTbl;

        public async Task<IEnumerable<SizeModel>> TabAsync()
        {
            return await context.SizeModelTbl.ToListAsync();
        }

        public async Task<SizeModel> GetByIdAsync(Guid LikeId)
        {
            SizeModel Data = await context.SizeModelTbl.FirstOrDefaultAsync(x => x.SizeId == LikeId);
            return Data;
        }

        public async Task<SizeModel> AddAsync(SizeModel _Like)
        {
           await context.SizeModelTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<SizeModel> RemoveAsync(Guid LikeId)
        {
            SizeModel Data = await context.SizeModelTbl.FirstOrDefaultAsync(x => x.SizeId == LikeId);
            if (Data != null)
            {
                context.SizeModelTbl.Remove(Data);
              await  context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<SizeModel> UpdaAsync(SizeModel _CategoryModel)
        {
            SizeModel Data = await context.SizeModelTbl.FirstOrDefaultAsync(x => x.SizeId == _CategoryModel.SizeId);
            if (Data != null)
            {
                Data.Size = _CategoryModel.Size;


                var save = context.SizeModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
