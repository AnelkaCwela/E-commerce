using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface ISupplierType
    {
        #region TabAsync
        Task<IQueryable<SupplierTypeModel>> TabAsync();

#endregion

        #region GetByIdAsync
        Task<SupplierTypeModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<SupplierTypeModel> AddAsync(SupplierTypeModel _Oder);
#endregion

        #region RemoveAsync
        Task<SupplierTypeModel> RemoveAsync(Guid OderId);
#endregion

        #region UpdaAsync
        Task<SupplierTypeModel> UpdaAsync(SupplierTypeModel OderId);
        #endregion
    }
}
