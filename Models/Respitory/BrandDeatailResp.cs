using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class BrandDeatailResp : IBrandDeatail
    {
        private readonly DBCONTEX context;
        public BrandDeatailResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<BrandDeatailModel> BrandDeatailTbl;

        public async Task<IQueryable<BrandDeatailModel>> TabAsync()
        {
            return (IQueryable<BrandDeatailModel>)await context.BrandDeatailTbl.ToListAsync();
        }

        public async Task<BrandDeatailModel> GetByIdAsync(Guid BrandId)
        {
          return await context.BrandDeatailTbl.FirstOrDefaultAsync(x => x.BrandDeatailId == BrandId);
            
        }

        public async Task<BrandDeatailModel> AddAsync(BrandDeatailModel _BrandModel)
        {
           await context.BrandDeatailTbl.AddAsync(_BrandModel);
           await context.SaveChangesAsync();
            return _BrandModel;
        }

        public async Task<BrandDeatailModel> RemoveAsync(Guid BrandId)
        {
           var Data = await context.BrandDeatailTbl.FirstOrDefaultAsync(x => x.BrandDeatailId == BrandId);
            if (Data != null)
            {
                context.BrandDeatailTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<BrandDeatailModel> UpdatAsync(BrandDeatailModel _BrandModel)
        {
            BrandDeatailModel Data = await context.BrandDeatailTbl.FirstOrDefaultAsync(x => x.BrandDeatailId == _BrandModel.BrandDeatailId);
            if (Data != null)
            {
                Data.BussneName = _BrandModel.BussneName;
                Data.RegistrationDate = _BrandModel.RegistrationDate
                    
                    ;
                Data.IdentityDocument = _BrandModel.IdentityDocument;
                Data.BussnessRegistration = _BrandModel.BussnessRegistration;
                var save = context.BrandDeatailTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
