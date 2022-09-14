using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IReserve
    {
        #region TabAsync
        Task<IQueryable<ReserveModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<ReserveModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<ReserveModel> AddAsync(ReserveModel _Oder);
        #endregion

        #region RemoveAsync
        Task<ReserveModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<ReserveModel> UpdaAsync(ReserveModel OderId);
        #endregion

    }
}
