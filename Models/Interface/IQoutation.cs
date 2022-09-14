using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface IQoutation
    {
        #region TabAsync
        Task<IQueryable<QuotationModel>> TabAsync();
        #endregion
        //#region GetByBoolAsync
        //Task<bool> GetRefranceAsync(string text);
        //#endregion
        #region GetCustomerAsync

        Task<QuotationModel> GetCustomerAsync(Guid LikeId);
        #endregion

        #region GetByIdAsync
        Task<QuotationModel> GetByIdAsync(Guid QoutationId);
        #endregion

        #region AddAsync
        Task<QuotationModel> AddAsync(QuotationModel _Qoutation);
        #endregion

        #region RemoveAsync
        Task<QuotationModel> RemoveAsync(Guid QoutationId);
        #endregion

        #region UpdaAsync
        Task<QuotationModel> UpdaAsync(QuotationModel QoutationId);
        #endregion
    }
}
