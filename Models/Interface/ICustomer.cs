using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface ICustomer
    {
        #region TabAsync
        Task<IQueryable<CustomerModel>> TabAsync();
        #endregion

        #region GetByUserNameAsync
        Task<CustomerModel> GetByUserName(string UserId);
        #endregion

        #region AddAsync
        Task<CustomerModel> AddAsync(CustomerModel _UserModel);
        #endregion
        //CustomerModel Remove(int UserId);
        //CustomerModel UpdateCust(CustomerModel _UserModel);

    }
}
