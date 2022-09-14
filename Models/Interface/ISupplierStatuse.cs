using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface ISupplierStatuse
    {
        #region TabAsync
        Task<IQueryable<SupplierStatuseModel>> TabAsync();

#endregion

        #region GetByIdAsync
        Task<SupplierStatuseModel> GetByIdAsync(Guid OderId);
#endregion

        #region AddAsync
        Task<SupplierStatuseModel> AddAsync(SupplierStatuseModel _Oder);
#endregion

        #region RemoveAsync
        Task<SupplierStatuseModel> RemoveAsync(Guid OderId);
#endregion

        #region UpdaAsync
        Task<SupplierStatuseModel> UpdaAsync(SupplierStatuseModel OderId);
        #endregion
    }
}
