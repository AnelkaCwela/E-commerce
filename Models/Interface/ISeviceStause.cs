using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface ISeviceStause
    {
        #region TabAsync
        Task<IQueryable<SeviceStauseModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<SeviceStauseModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<SeviceStauseModel> AddAsync(SeviceStauseModel _Oder);
        #endregion

        #region RemoveAsync
        Task<SeviceStauseModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<SeviceStauseModel> UpdaAsync(SeviceStauseModel OderId);
        #endregion
    }
}
