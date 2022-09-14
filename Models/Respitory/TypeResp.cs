using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System.Threading.Tasks;

namespace OurShop.Models.Respitory
{
    public class TypeResp : IType
    {
        private readonly DBCONTEX context;
        public TypeResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<TypeModel> TypeModelTbl;

        public async Task<IEnumerable<TypeModel>> TabAsync()
        {
            return await context.TypeModelTbl.ToListAsync();
        }

        public async Task<TypeModel> GetByIdAsync(Guid LikeId)
        {
            TypeModel Data = await context.TypeModelTbl.FirstOrDefaultAsync(x => x.TypeId == LikeId);
            return Data;
        }

        public async Task<TypeModel> AddAsync(TypeModel _Like)
        {
           await context.TypeModelTbl.AddAsync(_Like);
           await context.SaveChangesAsync();
            return _Like;
        }

        public async Task<TypeModel> RemoveAsync(Guid LikeId)
        {
            TypeModel Data = await context.TypeModelTbl.FirstOrDefaultAsync(x => x.TypeId == LikeId);
            if (Data != null)
            {
                context.TypeModelTbl.Remove(Data);
                context.SaveChanges();
            }
            return Data;
        }

        public async Task<TypeModel> UpdaAsync(TypeModel _CategoryModel)
        {
            TypeModel Data = await context.TypeModelTbl.FirstOrDefaultAsync(x => x.TypeId == _CategoryModel.TypeId);
            if (Data != null)
            {
                Data.TypeName = _CategoryModel.TypeName;
                //Data.Lat = _CategoryModel.Lat;
                //Data.Zoom = _CategoryModel.Zoom;
                ////Data. = _CategoryModel.Long;
                //Data.Long = _CategoryModel.Long;
                //Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.TypeModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
            }
            return Data;
        }


    }
}
