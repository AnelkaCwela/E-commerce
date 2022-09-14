using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface ISupplier
    {
        #region TabAsync
        Task<IQueryable<SupplierModel>> TabAsync();
        #endregion
        #region GetByEmailAsync
        Task<SupplierModel> GetByEmailAsync(string email);
        #endregion
        #region GetByIdAsync
        Task<SupplierModel> GetByIdAsync(Guid UserId);
        #endregion
        #region GetByIteamAsync
        Task<SupplierModel> GetByIteamAsync(Guid UserId);
        #endregion
        #region AddAsync
        Task<SupplierModel> AddAsync(SupplierModel _UserModel);
        #endregion
        #region RemoveAsync
        Task<SupplierModel> RemoveAsync(Guid UserId);
        #endregion
        #region UpdatAsync
        Task<SupplierModel> UpdatAsync(SupplierModel _UserModel);
        #endregion
    }
}
