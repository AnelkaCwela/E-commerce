using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class OderTypeResp : IOderType
    {

        private readonly DBCONTEX context;
        public OderTypeResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<OderTypeModel> OderTypeTbl;

        public async Task<IQueryable<OderTypeModel>> TabAsync()
        {
            return (IQueryable<OderTypeModel>)await context.OderTypeTbl.ToListAsync();
        }

        public async Task<OderTypeModel> GetByIdAsync(Guid LikeId)
        {
            return await context.OderTypeTbl.FirstOrDefaultAsync(x => x.OderTypeId == LikeId);
        
        }

        public async Task<OderTypeModel> AddAsync(OderTypeModel _Like)
        {
            await context.OderTypeTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<OderTypeModel> RemoveAsync(Guid LikeId)
        {
            OderTypeModel Data = await context.OderTypeTbl.FirstOrDefaultAsync(x => x.OderTypeId == LikeId);
            if (Data != null)
            {
                context.OderTypeTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<OderTypeModel> UpdaAsync(OderTypeModel _CategoryModel)
        {
            OderTypeModel Data = await context.OderTypeTbl.FirstOrDefaultAsync(x => x.OderTypeId == _CategoryModel.OderTypeId);
            if (Data != null)
            {
                Data.OderType = _CategoryModel.OderType;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.OderTypeTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }
    }
}
