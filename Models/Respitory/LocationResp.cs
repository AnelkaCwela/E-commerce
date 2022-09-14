using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class LocationResp : ILocation
    {
        private readonly DBCONTEX context;
        public LocationResp(DBCONTEX _context)
        {
            context = _context;
        }

       
        public async Task<IQueryable<LocationModel>> TabAsync()
        {
            return (IQueryable<LocationModel>)await context.LocationModelTbl.ToListAsync();
        }

        public async Task<LocationModel> GetByIdAsync(Guid BrandId)
        {
            return await context.LocationModelTbl.FirstOrDefaultAsync(x => x.LocationId == BrandId);
        }

        public async Task<LocationModel> AddAsync(LocationModel _BrandModel)
        {
            var brandModel = await context.LocationModelTbl.AddAsync(_BrandModel);
            await context.SaveChangesAsync();
            return brandModel.Entity;
        }

        public async Task<LocationModel> RemoveAsync(Guid BrandId)
        {
            var data = await context.LocationModelTbl.FirstOrDefaultAsync(x => x.LocationId == BrandId);
            if (data != null)
            {
                context.LocationModelTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<LocationModel> UpdatAsync(LocationModel _CategoryModel)
        {
            var Data = await context.LocationModelTbl.FirstOrDefaultAsync(x => x.LocationId == _CategoryModel.LocationId);
            if (Data != null)
            {
                Data.Address = _CategoryModel.Address;
                Data.Lat = _CategoryModel.Lat;
                Data.Zoom = _CategoryModel.Zoom;
                //Data. = _CategoryModel.Long;
                Data.Long = _CategoryModel.Long;
                Data.SupplierId = _CategoryModel.SupplierId;

                var save = context.LocationModelTbl.Attach(Data);
                save.State = EntityState.Modified;
             
                await context.SaveChangesAsync();
              
            }
            return _CategoryModel;
        }

        public async Task<LocationModel> GetByAddressdAsync(string BrandId)
        {
            return await context.LocationModelTbl.FirstOrDefaultAsync(x => x.Address == BrandId);
        }
    }
}
