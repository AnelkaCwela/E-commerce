using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface IServiceCategory
    {
        #region TabAsync
        Task<IQueryable<ServiceCategoryModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<ServiceCategoryModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<ServiceCategoryModel> AddAsync(ServiceCategoryModel _Oder);
        #endregion

        #region RemoveAsync
        Task<ServiceCategoryModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<ServiceCategoryModel> UpdaAsync(ServiceCategoryModel OderId);
        #endregion

    }
}
