using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface IType
    {
        #region  TabAsync
        Task<IEnumerable<TypeModel>> TabAsync();

         #endregion

        #region GetByIdAsync
        Task<TypeModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<TypeModel> AddAsync(TypeModel _Oder);
        #endregion

        #region RemoveAsync
        Task<TypeModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<TypeModel> UpdaAsync(TypeModel OderId);
        #endregion
    }
}
