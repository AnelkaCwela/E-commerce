using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IBrandDeatail
    {
        #region TabAsync
        Task<IQueryable<BrandDeatailModel>> TabAsync();
        #endregion

        #region GetByIdAsync
        Task<BrandDeatailModel> GetByIdAsync(Guid BrandId);
        #endregion
        #region AddAsync
        Task<BrandDeatailModel> AddAsync(BrandDeatailModel _BrandModel);
        #endregion
        #region RemoveAsync
        Task<BrandDeatailModel> RemoveAsync(Guid BrandId);
        #endregion
        #region UpdatAsync
        Task<BrandDeatailModel> UpdatAsync(BrandDeatailModel _BrandModel);
        #endregion
    }
}
