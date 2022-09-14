using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.Interface;
using OurShop.Models.DataModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class QoutationDetailzResp : IQoutationDetailz
    {
        private readonly DBCONTEX context;
        public QoutationDetailzResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<QuotationDetailModel> QuotationDetailModelTbl;

        public async Task<IQueryable<QuotationDetailModel>> TabAsync()
        {
            return (IQueryable<QuotationDetailModel>)await context.QuotationDetailModelTbl.ToListAsync();
        }

        public async Task<QuotationDetailModel> GetByIdAsync(Guid LikeId)
        {
            QuotationDetailModel Data = await context.QuotationDetailModelTbl.FirstOrDefaultAsync(x => x.QoutationDetailId == LikeId);
            return Data;
        }

        public async Task<QuotationDetailModel> AddAsync(QuotationDetailModel _Like)
        {
           await context.QuotationDetailModelTbl.AddAsync(_Like);
          await  context.SaveChangesAsync();
            return _Like;
        }

        public async Task<QuotationDetailModel> RemoveAsync(Guid LikeId)
        {
            QuotationDetailModel Data = await context.QuotationDetailModelTbl.FirstOrDefaultAsync(x => x.QoutationDetailId == LikeId);
            if (Data != null)
            {
                context.QuotationDetailModelTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<QuotationDetailModel> UpdaAsync(QuotationDetailModel _CategoryModel)
        {
            QuotationDetailModel Data = await context.QuotationDetailModelTbl.FirstOrDefaultAsync(x => x.QoutationDetailId == _CategoryModel.QoutationDetailId);
            if (Data != null)
            {
                Data.IteamDetailId = _CategoryModel.IteamDetailId;
                Data.QoutationId = _CategoryModel.QoutationId;
                Data.Quantty = _CategoryModel.Quantty;
                

                var save = context.QuotationDetailModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
