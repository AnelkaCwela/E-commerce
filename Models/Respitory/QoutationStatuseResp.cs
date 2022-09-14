using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class QoutationStatuseResp : IQoutationStatuse
    {
        private readonly DBCONTEX context;
        public QoutationStatuseResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<QoutationStatuseModel> QoutationStatuseTbl;

        public async Task<IQueryable<QoutationStatuseModel>> TabAsync()
        {
            return (IQueryable<QoutationStatuseModel>)await context.QoutationStatuseTbl.ToListAsync();
        }

        public async Task<QoutationStatuseModel> GetByIdAsync(Guid LikeId)
        {
            QoutationStatuseModel Data = await context.QoutationStatuseTbl.FirstOrDefaultAsync(x => x.QoutationStatuseId == LikeId);
            return Data;
        }

        public async Task<QoutationStatuseModel> AddAsync(QoutationStatuseModel _Like)
        {
           await context.QoutationStatuseTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<QoutationStatuseModel> RemoveAsync(Guid LikeId)
        {
            QoutationStatuseModel Data = await context.QoutationStatuseTbl.FirstOrDefaultAsync(x => x.QoutationStatuseId == LikeId);
            if (Data != null)
            {
                context.QoutationStatuseTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<QoutationStatuseModel> UpdaAsync(QoutationStatuseModel _CategoryModel)
        {
            QoutationStatuseModel Data = await context.QoutationStatuseTbl.FirstOrDefaultAsync(x => x.QoutationStatuseId == _CategoryModel.QoutationStatuseId);
            if (Data != null)
            {
                Data.QoutationStatuse = _CategoryModel.QoutationStatuse;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.QoutationStatuseTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }

    }
}
