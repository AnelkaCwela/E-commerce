using System;
using System.Collections.Generic;
using OurShop.Models.DataModel;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface IDeliveryStatuse
    {
        #region TabAsync
        Task<IQueryable<DeliveryStatuseModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<DeliveryStatuseModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<DeliveryStatuseModel> AddAsync(DeliveryStatuseModel _Oder);
        #endregion

        #region RemoveAsync
        Task<DeliveryStatuseModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<DeliveryStatuseModel> UpdaAsync(DeliveryStatuseModel OderId);
        #endregion
    }
}
