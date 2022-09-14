using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface IService
    {
        #region TabAsync
        Task<IQueryable<ServiceModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<ServiceModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<ServiceModel> AddAsync(ServiceModel _Oder);
        #endregion

        #region RemoveAsync
        Task<ServiceModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<ServiceModel> UpdaAsync(ServiceModel OderId);
        #endregion
    }
}
