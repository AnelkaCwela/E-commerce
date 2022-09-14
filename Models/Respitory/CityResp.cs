using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class CityResp : ICity
    {
        private readonly DBCONTEX context;
        public CityResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<CityModel> CityModelTbl;

        public async Task<IEnumerable<CityModel>> TabAsync()
        {
            return await context.CityModelTbl.ToListAsync();
        }

        public async Task<CityModel> AddAsync(CityModel _City)
        {
            await context.CityModelTbl.AddAsync(_City);
            await context.SaveChangesAsync();
            return _City;
        }

        //public async Task<CityModel> GetByIdAsync(Guid CityId)
        //{


        //    return await CityModelTbl.FirstOrDefaultAsync(x => x.CityId == CityId);
           

        //}
    }
}
