using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IBrand
    {
        #region TabAsync
        Task<IEnumerable<BrandModel>> TabAsync();
       #endregion

        #region GetByIdAsync
        Task<BrandModel> GetByIdAsync(Guid BrandId);
        #endregion
        #region AddAsync
        Task<BrandModel> AddAsync(BrandModel _BrandModel);
        #endregion
        #region RemoveAsync
        Task<BrandModel> RemoveAsync(Guid BrandId);
        #endregion
        #region UpdatAsync
        Task<BrandModel> UpdatAsync(BrandModel _BrandModel);
        #endregion
    }
}
