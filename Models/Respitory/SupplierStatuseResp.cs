using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class SupplierStatuseResp : ISupplierStatuse
    {
        private readonly DBCONTEX context;
        public SupplierStatuseResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<SupplierStatuseModel> SupplierStatuseTbl;

        public async Task<IQueryable<SupplierStatuseModel>> TabAsync()
        {
            return (IQueryable<SupplierStatuseModel>)await context.SupplierStatuseTbl.ToListAsync();
        }

        public async Task<SupplierStatuseModel> GetByIdAsync(Guid LikeId)
        {
            SupplierStatuseModel Data = await context.SupplierStatuseTbl.FirstOrDefaultAsync(x => x.SupplierStatuseId == LikeId);
            return Data;
        }

        public async Task<SupplierStatuseModel> AddAsync(SupplierStatuseModel _Like)
        {
           await context.SupplierStatuseTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<SupplierStatuseModel> RemoveAsync(Guid LikeId)
        {
            SupplierStatuseModel Data = await context.SupplierStatuseTbl.FirstOrDefaultAsync(x => x.SupplierStatuseId == LikeId);
            if (Data != null)
            {
                context.SupplierStatuseTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<SupplierStatuseModel> UpdaAsync(SupplierStatuseModel _CategoryModel)
        {
            SupplierStatuseModel Data = await context.SupplierStatuseTbl.FirstOrDefaultAsync(x => x.SupplierStatuseId == _CategoryModel.SupplierStatuseId);
            if (Data != null)
            {
                Data.SupplierStatuse = _CategoryModel.SupplierStatuse;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.SupplierStatuseTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

              await  context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
