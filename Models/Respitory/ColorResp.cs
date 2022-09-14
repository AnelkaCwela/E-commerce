using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
namespace OurShop.Models.Respitory
{
    public class ColorResp : IColor
    {
        private readonly DBCONTEX context;
        public ColorResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<ColorModel> ColorModelTbl;

        public async Task<IEnumerable<ColorModel>> TabAsync()
        {
            return await context.ColorModelTbl.ToListAsync();
            
        }

        public async Task<ColorModel> AddAsync(ColorModel _Color)
        {
            await context.ColorModelTbl.AddAsync(_Color);
           await context.SaveChangesAsync();
            return _Color;
        }
    }
}
