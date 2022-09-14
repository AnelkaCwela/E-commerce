using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Respitory
{
    public class BrandResp : IBrand
    {
        private readonly DBCONTEX context;
        public BrandResp(DBCONTEX _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<BrandModel>> TabAsync()
        {
            return await context.BrandModelTbl.ToListAsync();
        }

        public async Task<BrandModel> GetByIdAsync(Guid BrandId)
        {
            return await context.BrandModelTbl.FirstOrDefaultAsync(x => x.BrandId == BrandId);
        }

        public async Task<BrandModel> AddAsync(BrandModel _BrandModel)
        {
            var brandModel= await context.BrandModelTbl.AddAsync(_BrandModel);
            await context.SaveChangesAsync();
            return brandModel.Entity;
        }

        public async Task<BrandModel> RemoveAsync(Guid BrandId)
        {
         var data=  await context.BrandModelTbl.FirstOrDefaultAsync(x => x.BrandId == BrandId);
            if(data!=null)
            {
                 context.BrandModelTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<BrandModel> UpdatAsync(BrandModel _BrandModel)
        {
            var Data = await context.BrandModelTbl.FirstOrDefaultAsync(x => x.BrandId == _BrandModel.BrandId);
            if (Data != null)
            {
                Data.BrandName = _BrandModel.BrandName;
                Data.BrandSlogn = _BrandModel.BrandSlogn;
                Data.BrandLogo = _BrandModel.BrandLogo;
                var save = context.BrandModelTbl.Attach(Data);
                save.State =EntityState.Modified;
                await context.SaveChangesAsync();
                //await context.SaveChangesAsync();
            }
            return _BrandModel;
        }
    }
}
