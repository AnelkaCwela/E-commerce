using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Respitory
{
    public class IteamDetailResp : IIteamDetail
    {
        private readonly DBCONTEX context;
        public IteamDetailResp(DBCONTEX _context)
        {
            context = _context;
        }
        public async Task<IteamDetailModel> Add(IteamDetailModel _Oder)
        {
            var brandModel = await context.IteamDetailTbl.AddAsync(_Oder);
            await context.SaveChangesAsync();
            return brandModel.Entity;

        }

        public async Task<IteamDetailModel> GetById(Guid OderId)
        {
            return await context.IteamDetailTbl.FirstOrDefaultAsync(x => x.IteamDetailId == OderId);
        }
        public async Task<IteamDetailModel> CheckAsync(Guid Id ,Guid ColorId ,Guid SizeId)
        {
            var Iteam = await context.IteamDetailTbl.Where(x => x.IteamId == Id).ToListAsync();
            IteamDetailModel Data = new IteamDetailModel();
            if (Iteam!=null)
            {
                var data = Iteam.Where(i => i.SizeId == SizeId);
                if(data!=null)
                {
                    Data = data.FirstOrDefault(i=>i.ColorId== ColorId);
                }
                

            }

            return Data;
        }

        public async Task<IteamDetailModel> GetByItemId(Guid OderId)
        {
            return await context.IteamDetailTbl.FirstOrDefaultAsync(x => x.IteamId == OderId);
        }

        public async Task<IteamDetailModel> Remove(Guid OderId)
        {
            var data = await context.IteamDetailTbl.FirstOrDefaultAsync(x => x.IteamDetailId == OderId);
            if (data != null)
            {
                context.IteamDetailTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<IEnumerable<IteamDetailModel>> GetListByItemIdsync(Guid id)
        {
            return await context.IteamDetailTbl.Where(x=>x.IteamId==id).ToListAsync();
        }
        public async Task<IEnumerable<IteamDetailModel>> Tab()
        {
            return await context.IteamDetailTbl.ToListAsync();
        }

        public async Task<bool> UpdaItem(Guid id ,int item,bool Stock)
        {
            var Data = await context.IteamDetailTbl.FirstOrDefaultAsync(x => x.IteamDetailId == id);
            if (Data != null)
            {
                Data.Stock = Stock;
                Data.NoOfIteam = item;              
                var save = context.IteamDetailTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
                //await context.SaveChangesAsync();
            }
            return Stock;
        }
        //public async Task<IteamDetailModel> UpdaPrice(Guid id, decimal Price)
        //{
        //    var Data = await context.IteamDetailTbl.Where(x => x.IteamDetailId == id)
        //    if (Data != null)
        //    {
        //        // Data.IteamId = OderId.IteamId;
        //        Data.Price = Price;
                
        //        var save = context.IteamDetailTbl.Attach(Data);
        //        save.State =EntityState.Modified;
        //        await context.SaveChangesAsync();
        //        //await context.SaveChangesAsync();
        //    }
        //    return Data;
        //}
        public async Task<IteamDetailModel> Upda(IteamDetailModel OderId)
        {
            var Data = await context.IteamDetailTbl.FirstOrDefaultAsync(x => x.IteamDetailId == OderId.IteamDetailId);
            if (Data != null)
            {
               // Data.IteamId = OderId.IteamId;
                Data.NoOfIteam = OderId.NoOfIteam;
                Data.SizeId = OderId.SizeId;
                Data.ColorId = OderId.ColorId;
                var save = context.IteamDetailTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
                //await context.SaveChangesAsync();
            }
            return OderId;
        }
    }
}
