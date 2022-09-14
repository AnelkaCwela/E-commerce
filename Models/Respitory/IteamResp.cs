using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class IteamResp : IIteam
    {
        private readonly DBCONTEX context;
        public IteamResp(DBCONTEX _context)
        {
            context = _context;
        }

        //public async Task<IEnumerable<IteamModel>> TaBAsync()
        //{
        //    return await context.IteamModelTbl.ToListAsync();
        //}

        //public async Task<IEnumerable<IteamModel>> TaBAsync()
        //{
        //    return await context.IteamModelTbl.ToListAsync();
        //}
        public async Task<IEnumerable<IteamModel>> TabAsync()
        {
            return await context.IteamModelTbl.ToListAsync();
        }
        public async Task<IEnumerable<IteamModel>> GetListItemByCategoryAsync(Guid SupplierId)////////
        {
            return await context.IteamModelTbl.Where(i => i.CategoryId == SupplierId).ToListAsync();
        }
        public async Task<IEnumerable<IteamModel>> TabGroupAsync(Guid SupplierId)////////
        {
            return await context.IteamModelTbl.Where(i=>i.SupplierId== SupplierId).ToListAsync();
        }
        public async Task<IteamModel> GetByIdAsync(Guid IteamId)
        {
            return await context.IteamModelTbl.FirstOrDefaultAsync(x => x.IteamId == IteamId);
        }

        public async Task<IteamModel> AddAsync(IteamModel _Iteam)
        {
            var brandModel = await context.IteamModelTbl.AddAsync(_Iteam);
            await context.SaveChangesAsync();
            return brandModel.Entity;
        }

        public async Task<IteamModel> RemoveAsync(Guid IteamId)
        {
            var data = await context.IteamModelTbl.FirstOrDefaultAsync(x => x.IteamId == IteamId);
            if (data != null)
            {
                context.IteamModelTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<IteamModel> UpdatAsync(IteamModel IteamId)
        {
            var Data = await context.IteamModelTbl.FirstOrDefaultAsync(x => x.IteamId == IteamId.IteamId);
            if (Data != null)
            {
                Data.ItermName = IteamId.ItermName;
                Data.Price = IteamId.Price;
                Data.DiscountPrice = IteamId.DiscountPrice;
                Data.CategoryId = IteamId.CategoryId;
                Data.GanderId = IteamId.GanderId;                          
                var save = context.IteamModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await context.SaveChangesAsync();
               
            }
            return IteamId;
        }
        public async Task<IteamModel> UpdatQuaAsync(IteamModel IteamId)
        {
            var Data = await context.IteamModelTbl.FirstOrDefaultAsync(x => x.IteamId == IteamId.IteamId);
            if (Data != null)
            {
                Data.Price = IteamId.Price;
                
                var save = context.IteamModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await context.SaveChangesAsync();

            }
            return IteamId;
        }
        public async Task<bool> UpdatStocksync(Guid id, bool stock)
        {
            var Data = await context.IteamModelTbl.FirstOrDefaultAsync(x => x.IteamId == id);
            if (Data != null)
            {
                Data.InStock = stock;

                var save = context.IteamModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await context.SaveChangesAsync();

            }
            return stock;
        }



        //public async Task<bool> GetRefranceAsync(string text)
        //{
        //    return (IQueryable<IteamModel>)await context.IteamModelTbl.Where(i => i. == SupplierId).ToListAsync();
        //}
    }
}
