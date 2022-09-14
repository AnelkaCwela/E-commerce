using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface IQoutationDetailz
    {
        #region TabAsync
        Task<IQueryable<QuotationDetailModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<QuotationDetailModel> GetByIdAsync(Guid QoutationDetailId);
        #endregion

        #region AddAsync
        Task<QuotationDetailModel> AddAsync(QuotationDetailModel _QoutationDetail);
        #endregion

        #region RemoveAsync
        Task<QuotationDetailModel> RemoveAsync(Guid QoutationDetailId);
        #endregion

        #region UpdaAsync
        Task<QuotationDetailModel> UpdaAsync(QuotationDetailModel QoutationDetailId);
        #endregion
    }
}
