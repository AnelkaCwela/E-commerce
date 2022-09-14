using System;
using System.Collections.Generic;
using OurShop.Models.DataModel;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface IDelivery
    {
        #region TabAsync
        Task<IQueryable<DeliveryModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<DeliveryModel> GetByIdAsync(Guid DeliveryId);
        #endregion

        #region AddAsync
        Task<DeliveryModel> AddAsync(DeliveryModel _Delivery);
        #endregion

        #region RemoveAsync
        Task<DeliveryModel> RemoveAsync(Guid DeliveryId);
        #endregion

        #region UpdatAsync
        Task<DeliveryModel> UpdatAsync(DeliveryModel _Delivery);
        #endregion
    }
}
