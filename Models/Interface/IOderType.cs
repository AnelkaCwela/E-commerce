using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IOderType
    {
        #region TabAsync
        Task<IQueryable<OderTypeModel>> TabAsync();

        #endregion

        #region GetByIdAsync
        Task<OderTypeModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<OderTypeModel> AddAsync(OderTypeModel _Oder);
        #endregion

        #region RemoveAsync
        Task<OderTypeModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<OderTypeModel> UpdaAsync(OderTypeModel OderId);
        #endregion
    }
}
