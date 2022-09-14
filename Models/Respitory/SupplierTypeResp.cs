using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class SupplierTypeResp : ISupplierType
    {
        private readonly DBCONTEX context;
        public SupplierTypeResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<SupplierTypeModel> SupplierTypeTbl;

        public async Task<IQueryable<SupplierTypeModel>> TabAsync()
        {
            return (IQueryable<SupplierTypeModel>)await context.SupplierTypeTbl.ToListAsync();
        }

        public async Task<SupplierTypeModel> GetByIdAsync(Guid LikeId)
        {
            SupplierTypeModel Data = await context.SupplierTypeTbl.FirstOrDefaultAsync(x => x.SupplierTypeId == LikeId);
            return Data;
        }

        public async Task<SupplierTypeModel> AddAsync(SupplierTypeModel _Like)
        {
           await context.SupplierTypeTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<SupplierTypeModel> RemoveAsync(Guid LikeId)
        {
            SupplierTypeModel Data = await context.SupplierTypeTbl.FirstOrDefaultAsync(x => x.SupplierTypeId == LikeId);
            if (Data != null)
            {
                context.SupplierTypeTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<SupplierTypeModel> UpdaAsync(SupplierTypeModel _CategoryModel)
        {
            SupplierTypeModel Data = await context.SupplierTypeTbl.FirstOrDefaultAsync(x => x.SupplierTypeId == _CategoryModel.SupplierTypeId);
            if (Data != null)
            {
                Data.SupplierType = _CategoryModel.SupplierType;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.SupplierTypeTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
