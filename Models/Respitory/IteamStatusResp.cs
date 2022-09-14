using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;

namespace OurShop.Models.Respitory
{
    public class IteamStatusResp : IIteamStatuse
    {
        private readonly DBCONTEX context;
        public IteamStatusResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IEnumerable<IteamStatuseModel> IteamStatuseTbl;

        public async Task<IEnumerable<IteamStatuseModel>> TabAsync()
        {
            return await context.IteamStatuseTbl.ToListAsync();
        }

        public async Task<IteamStatuseModel> AddAsync(IteamStatuseModel _Color)
        {
           await context.IteamStatuseTbl.AddAsync(_Color);
           await context.SaveChangesAsync();
            return _Color;
        }
    }
}
