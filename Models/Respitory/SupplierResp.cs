using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class SupplierResp : ISupplier
    {

        private readonly DBCONTEX context;
        public SupplierResp(DBCONTEX _context)
        {
            context = _context;
        }



        public async Task<IQueryable<SupplierModel>> TabAsync()
        {
            return (IQueryable<SupplierModel>)await context.SupplierModelTbl.ToListAsync();
        }

        public async Task<SupplierModel> GetByEmailAsync(string email)
        {
            return await context.SupplierModelTbl.FirstOrDefaultAsync(x => x.SupplierUserName == email);
        }

        public async Task<SupplierModel> GetByIdAsync(Guid UserId)
        {
            return await context.SupplierModelTbl.FirstOrDefaultAsync(x => x.SupplierId == UserId);
        }
        public async Task<SupplierModel> GetByIteamAsync(Guid UserId)
        {
            return await context.SupplierModelTbl.FirstOrDefaultAsync(x => x.SupplierId == UserId);
        }
        public async Task<SupplierModel> AddAsync(SupplierModel _UserModel)
        {
            var brandModel = await context.SupplierModelTbl.AddAsync(_UserModel);
            await context.SaveChangesAsync();
            return brandModel.Entity;
        }

        public async Task<SupplierModel> RemoveAsync(Guid UserId)
        {
            var data = await context.SupplierModelTbl.FirstOrDefaultAsync(x => x.SupplierId == UserId);
            if (data != null)
            {
                context.SupplierModelTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<SupplierModel> UpdatAsync(SupplierModel _CategoryModel)
        {
            var Data = await context.SupplierModelTbl.FirstOrDefaultAsync(x => x.SupplierId == _CategoryModel.SupplierId);
            if (Data != null)
            {
                Data.BrandId = _CategoryModel.BrandId;
                Data.SupplierStatuseId = _CategoryModel.SupplierStatuseId;
                Data.SupplierTypeId = _CategoryModel.SupplierTypeId;
                Data.BrandDeatailId = _CategoryModel.BrandDeatailId;
                Data.BrandId = _CategoryModel.BrandId;
                var save = context.SupplierModelTbl.Attach(Data);
                save.State =EntityState.Modified;
                await context.SaveChangesAsync();
                //await context.SaveChangesAsync();
            }
            return _CategoryModel;
        }
    }
}
