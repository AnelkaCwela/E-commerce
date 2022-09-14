using System;
using OurShop.Models.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface IResident
    {
        #region TabAsync
        Task<IQueryable<ResidentModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<ResidentModel> GetByIdAsync(Guid ColorId);
        #endregion
        #region GetBySupplIdAsync

        Task<ResidentModel> GetBySupplIdAsync(Guid ColorId);
        #endregion

        #region AddAsync
        Task<ResidentModel> AddAsync(ResidentModel _Color);
        #endregion

        #region RemoveAsync
        Task<ResidentModel> RemoveAsync(Guid ColorId);
        #endregion

        #region UpdaAsync
        Task<ResidentModel> UpdaAsync(ResidentModel ColorId);
        #endregion
    }
}
