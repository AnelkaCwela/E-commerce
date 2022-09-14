using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class GanderResp : IGander
    {
        private readonly DBCONTEX context;
        public GanderResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<CategoryModel> GanderModelTbl;
        public async Task<GanderModel> AddAsync(GanderModel _Imagel)
        {
             await context.GanderModelTbl.AddAsync(_Imagel);
           await context.SaveChangesAsync();
            return _Imagel;
        }


        public async Task<IEnumerable<GanderModel>> TabAsync()
        {
            return await context.GanderModelTbl.ToListAsync();
        }
    }
}
