using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class CategoryResp : ICategory
    {
        private readonly DBCONTEX context;
        public CategoryResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<CategoryModel> CategoryModelTbl;

        public async Task<IEnumerable<CategoryModel>> TabAsync()
        {
         
            return await context.CategoryModelTbl.ToListAsync();
        }
        public async Task<IEnumerable<CategoryModel>> ListByIdAsync(Guid Id)
        {

            return await context.CategoryModelTbl.Where(id=>id.TypeId==Id).ToListAsync();
        }

        public async Task<CategoryModel> GetByIdAsync(Guid CategoryId)
        {
            return await context.CategoryModelTbl.FirstOrDefaultAsync(x => x.CategoryId == CategoryId);
           
        }

        public async Task<CategoryModel> AddAsync(CategoryModel _CategoryModel)
        {
            await context.CategoryModelTbl.AddAsync(_CategoryModel);
           await context.SaveChangesAsync();
            return _CategoryModel;
        }

        public async Task<CategoryModel> RemoveAsync(Guid CategoryId)
        {
            CategoryModel Data = await context.CategoryModelTbl.FirstOrDefaultAsync(x => x.CategoryId == CategoryId);
            if (Data != null)
            {
                context.CategoryModelTbl.Remove(Data);
               await context.SaveChangesAsync();
            }
            return Data;
        }

        public async Task<CategoryModel> UpdatAsync(CategoryModel _CategoryModel)
        {
            CategoryModel Data = await context.CategoryModelTbl.FirstOrDefaultAsync(x => x.CategoryId == _CategoryModel.CategoryId);
            if (Data != null)
            {
                Data.CategoryName = _CategoryModel.CategoryName;
                Data.TypeId = _CategoryModel.TypeId;


                var save = context.CategoryModelTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               await context.SaveChangesAsync();
            }
            return Data;
        }
    }
}
