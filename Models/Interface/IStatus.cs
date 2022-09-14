using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface IStatus
    {
        #region TabAsync
        Task<IEnumerable<StatusModel>> TabAsync();

#endregion

        #region GetByIdAsync
        Task<StatusModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<StatusModel> AddAsync(StatusModel _Oder);
#endregion

        #region RemoveAsync
        Task<StatusModel> RemoveAsync(Guid OderId);
#endregion

        #region UpdaAsync
        Task<StatusModel> UpdaAsync(StatusModel OderId);
        #endregion
    }
}
