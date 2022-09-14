using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class ResidentResp : IResident
    {
        private readonly DBCONTEX context;
        public ResidentResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<ResidentModel> ResidentModelTbl;

        public async Task<IQueryable<ResidentModel>> TabAsync()
        {
            return (IQueryable<ResidentModel>) await context.ResidentModelTbl.ToListAsync();
        }
        public async Task<ResidentModel> GetBySupplIdAsync(Guid LikeId)
        {
           return await context.ResidentModelTbl.FirstOrDefaultAsync(x => x.SupplierId == LikeId);
           
        }


        public async Task<ResidentModel> GetByIdAsync(Guid LikeId)
        {
            ResidentModel Data = await context.ResidentModelTbl.FirstOrDefaultAsync(x => x.ResidentId == LikeId);
            return Data;
        }

        public async Task<ResidentModel> AddAsync(ResidentModel _Like)
        {
             await context.ResidentModelTbl.AddAsync(_Like);
           await context.SaveChangesAsync();

            return _Like;
        }

        public async Task<ResidentModel> RemoveAsync(Guid LikeId)
        {
            ResidentModel Data = await context.ResidentModelTbl.FirstOrDefaultAsync(x => x.ResidentId == LikeId);
            if (Data != null)
            {
                context.ResidentModelTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<ResidentModel> UpdaAsync(ResidentModel _CategoryModel)
        {
            ResidentModel Data = await context.ResidentModelTbl.FirstOrDefaultAsync(x => x.ResidentId == _CategoryModel.ResidentId);
            if (Data != null)
            {
                Data.CityId = _CategoryModel.CityId;
                //Data.ProvinceId = _CategoryModel.CityId;




                var save = context.ResidentModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }

       
    }
}
