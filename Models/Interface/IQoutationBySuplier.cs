using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IQoutationBySuplier
    {
        #region TabAsync
        Task<IQueryable<QoutationBySuplierModel>> TabAsync();

#endregion

        #region GetByIdAsync
        Task<QoutationBySuplierModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<QoutationBySuplierModel> AddAsync(QoutationBySuplierModel _Oder);
           #endregion

        #region RemoveAsync
        Task<QoutationBySuplierModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<QoutationBySuplierModel> UpdaAsync(QoutationBySuplierModel OderId);
        #endregion
    }
}
