using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;


namespace OurShop.Models.Interface
{
    public interface ILocation
    {
        #region TabAsync
        Task<IQueryable<LocationModel>> TabAsync();
        #endregion
        #region GetByAddressdAsync
        Task<LocationModel> GetByAddressdAsync(string BrandId);
        #endregion
        #region GetByIdAsync
        Task<LocationModel> GetByIdAsync(Guid BrandId);
        #endregion
        #region AddAsync
        Task<LocationModel> AddAsync(LocationModel _BrandModel);
        #endregion
        #region RemoveAsync
        Task<LocationModel> RemoveAsync(Guid BrandId);
        #endregion
        #region UpdatAsync
        Task<LocationModel> UpdatAsync(LocationModel _BrandModel);
        #endregion
    }
}
