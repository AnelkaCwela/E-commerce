using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace OurShop.Models.Respitory
{
    public class ContactDetailResp : IContactDetail
    {
        private readonly DBCONTEX context;
        public ContactDetailResp(DBCONTEX _context)
        {
            context = _context;
        }

        

        public async Task<IQueryable<ContactDetailModel>> TabAsync()
        {
            return (IQueryable<ContactDetailModel>)await context.ContactDetailTbl.ToListAsync();
        }

        public async Task<ContactDetailModel> GetByIdAsync(Guid UserId)
        {
            return await context.ContactDetailTbl.FirstOrDefaultAsync(x => x.ContactDetailId == UserId);
        }

        public async Task<ContactDetailModel> GetByBradIdAsync(Guid UserId)
        {
            return await context.ContactDetailTbl.FirstOrDefaultAsync(x => x.BrandId == UserId);
        }

        public async Task<ContactDetailModel> AddAsync(ContactDetailModel _UserModel)
        {
            var brandModel = await context.ContactDetailTbl.AddAsync(_UserModel);
            await context.SaveChangesAsync();
            return brandModel.Entity;
        }

        public async Task<ContactDetailModel> RemoveAsync(Guid UserId)
        {
            var data = await context.ContactDetailTbl.FirstOrDefaultAsync(x => x.ContactDetailId == UserId);
            if (data != null)
            {
                context.ContactDetailTbl.Remove(data);
                await context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<ContactDetailModel> UpdatAsync(ContactDetailModel _CategoryModel)
        {
            var Data = await context.ContactDetailTbl.FirstOrDefaultAsync(x => x.ContactDetailId == _CategoryModel.ContactDetailId);
            if (Data != null)
            {
                Data.EmailAddress = _CategoryModel.EmailAddress;
                Data.FaxNo = _CategoryModel.FaxNo;
                

                Data.TelNo = _CategoryModel.TelNo;
               
                var save = context.ContactDetailTbl.Attach(Data);
                save.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
                //await context.SaveChangesAsync();
            }
            return _CategoryModel;
        }
    }
}
