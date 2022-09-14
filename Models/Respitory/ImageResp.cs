using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class ImageResp : IImage
    {
        private readonly DBCONTEX context;
        public ImageResp(DBCONTEX _context)
        {
            context = _context;
        }
        public async Task<ImageModel> RemoveAsync(Guid IteamId)
        {
            var data = await context.ImageModelTbl.FirstOrDefaultAsync(x => x.ImageId == IteamId);
            if (data != null)
            {
                context.ImageModelTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<IEnumerable<ImageModel>> GetByIdAsync(Guid ImageId)
        {
            return await context.ImageModelTbl.Where(i => i.IteamId == ImageId).ToListAsync();
        }
        public async Task<IEnumerable<ImageModel>> GetListByItemIdsync(Guid id)
        {
            //Guid dd = new Guid("");
            return await context.ImageModelTbl.Where(x => x.IteamId == id).ToListAsync();
        }

        public async Task<IEnumerable<ImageModel>> TabAsync()
        {
            
            return await context.ImageModelTbl.ToListAsync();
        }
       
        public async Task<ImageModel> AddAsync(ImageModel _Imagel)

        {
          await context.ImageModelTbl.AddAsync(_Imagel);
            await context.SaveChangesAsync();
            return _Imagel;
        }
      
    }
}
