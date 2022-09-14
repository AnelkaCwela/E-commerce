using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
using OurShop.Models.Interface;
using Microsoft.EntityFrameworkCore;
namespace OurShop.Models.Respitory
{
    public class CustomerResp : ICustomer
    {
        private readonly DBCONTEX context;
        public CustomerResp(DBCONTEX _context)
        {
            context = _context;
        }

        public IQueryable<CustomerModel> CustomerModelTbl;

        public async Task<IQueryable<CustomerModel>> TabAsync()
        {
            return (IQueryable<CustomerModel>)await context.CustomerModelTbl.ToListAsync();
        }

        public async Task<CustomerModel> GetByUserName(string UserId)
        {
            return await context.CustomerModelTbl.FirstOrDefaultAsync(x => x.CustomerUserName == UserId);
    
        }

        public async Task<CustomerModel> AddAsync(CustomerModel _UserModel)
        {
           await context.CustomerModelTbl.AddAsync(_UserModel);
           
             await   context.SaveChangesAsync();
            return _UserModel;
        }
    }
}
