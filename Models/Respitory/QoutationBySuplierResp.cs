using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class QoutationBySuplierResp : IQoutationBySuplier
    {
        private readonly DBCONTEX context;
        public QoutationBySuplierResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<QoutationBySuplierModel> QoutationBySuplierTbl;

        public async Task<IQueryable<QoutationBySuplierModel>> TabAsync()
        {
            return (IQueryable<QoutationBySuplierModel>)await context.QoutationBySuplierTbl.ToListAsync();
        }

        public async Task<QoutationBySuplierModel> GetByIdAsync(Guid LikeId)
        {
            QoutationBySuplierModel Data = await context.QoutationBySuplierTbl.FirstOrDefaultAsync(x => x.QoutationStatuseSuplierId == LikeId);
            return Data;
        }

        public async Task<QoutationBySuplierModel> AddAsync(QoutationBySuplierModel _Like)
        {
          await  context.QoutationBySuplierTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<QoutationBySuplierModel> RemoveAsync(Guid LikeId)
        {
            QoutationBySuplierModel Data = await context.QoutationBySuplierTbl.FirstOrDefaultAsync(x => x.QoutationStatuseSuplierId == LikeId);
            if (Data != null)
            {
                context.QoutationBySuplierTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<QoutationBySuplierModel> UpdaAsync(QoutationBySuplierModel _CategoryModel)
        {
            QoutationBySuplierModel Data = await context.QoutationBySuplierTbl.FirstOrDefaultAsync(x => x.QoutationStatuseSuplierId == _CategoryModel.QoutationStatuseSuplierId);
            if (Data != null)
            {
                Data.QoutationStatuseSuplier = _CategoryModel.QoutationStatuseSuplier;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.QoutationBySuplierTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

              await  context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
