using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IQoutationStatuse
    {
        #region TabAsync
        Task<IQueryable<QoutationStatuseModel>> TabAsync();

        #endregion

        #region GetByIdAsync
        Task<QoutationStatuseModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<QoutationStatuseModel> AddAsync(QoutationStatuseModel _Oder);
        #endregion

        #region RemoveAsync
        Task<QoutationStatuseModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<QoutationStatuseModel> UpdaAsync(QoutationStatuseModel OderId);
        #endregion
    }
}
