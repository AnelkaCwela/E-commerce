using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class QoutationResp : IQoutation
    {
        private readonly DBCONTEX context;
        public QoutationResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<QuotationModel> QoutationModelTbl;

        public async Task<IQueryable<QuotationModel>> TabAsync()
        {
            return (IQueryable<QuotationModel>)await context.QoutationModelTbl.ToListAsync();
        }

        public async Task<QuotationModel> GetByIdAsync(Guid LikeId)
        {
            QuotationModel Data = await context.QoutationModelTbl.FirstOrDefaultAsync(x => x.QoutationId == LikeId);
            return Data;
        }
        public async Task<QuotationModel> GetCustomerAsync(Guid LikeId)
        {
            QuotationModel Data = await context.QoutationModelTbl.FirstOrDefaultAsync(x => x.CustomerId == LikeId);
            return Data;
        }
        public async Task<QuotationModel> AddAsync(QuotationModel _Like)
        {
           await context.QoutationModelTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<QuotationModel> RemoveAsync(Guid LikeId)
        {
            QuotationModel Data = await context.QoutationModelTbl.FirstOrDefaultAsync(x => x.QoutationId == LikeId);
            if (Data != null)
            {
                context.QoutationModelTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<QuotationModel> UpdaAsync(QuotationModel _CategoryModel)
        {
            QuotationModel Data = await context.QoutationModelTbl.FirstOrDefaultAsync(x => x.QoutationId == _CategoryModel.QoutationId);
            if (Data != null)
            {
                Data.CustomerId = _CategoryModel.CustomerId;
                Data.OderTypeId = _CategoryModel.OderTypeId;
               
                
                Data.QoutationDate = _CategoryModel.QoutationDate;
                Data.RefranceNo = _CategoryModel.RefranceNo;
                Data.QoutationStatuseId = _CategoryModel.QoutationStatuseId;
                Data.SubrbId = _CategoryModel.SubrbId;

                var save = context.QoutationModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }


    }
}
