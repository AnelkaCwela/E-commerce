using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IServiceType
    {

        #region TabAsync
        Task<IQueryable<ServiceTypeModel>> TabAsync();
#endregion

        #region GetByIdAsync

        Task<ServiceTypeModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<ServiceTypeModel> AddAsync(ServiceTypeModel _Oder);
#endregion

        #region RemoveAsync
        Task<ServiceTypeModel> RemoveAsync(Guid OderId);
#endregion

        #region UpdaAsync
        Task<ServiceTypeModel> UpdaAsync(ServiceTypeModel OderId);
        #endregion
    }
}
