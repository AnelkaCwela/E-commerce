using System;
using System.Collections.Generic;
using OurShop.Models.DataModel;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface ISubub
    {
        #region TabAsync
        Task<IQueryable<SububModel>> TabAsync();

#endregion

        #region GetByIdAsync
        Task<SububModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<SububModel> AddAsync(SububModel _Oder);
#endregion

        #region RemoveAsync
        Task<SububModel> RemoveAsync(Guid OderId);
#endregion

        #region UpdaAsync
        Task<SububModel> UpdaAsync(SububModel OderId);
        #endregion
    }
}
