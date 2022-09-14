using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IServiceReserveStatuse
    {
        #region TabAsync
        Task<IQueryable<ServiceReserveStatuseModel>> TabAsync();
#endregion

        #region GetByIdAsync

        Task<ServiceReserveStatuseModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<ServiceReserveStatuseModel> AddAsync(ServiceReserveStatuseModel _Oder);
#endregion

        #region RemoveAsync
        Task<ServiceReserveStatuseModel> RemoveAsync(Guid OderId);
#endregion

        #region UpdaAsync
        Task<ServiceReserveStatuseModel> UpdaAsync(ServiceReserveStatuseModel OderId);
        #endregion
    }
}
