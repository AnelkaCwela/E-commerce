using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IRating
    {
        #region TabAsync
        Task<IQueryable<RatingModel>> TabAsync();
        #endregion

        #region GetByIdAsync

        Task<RatingModel> GetByIdAsync(Guid OderId);
        #endregion

        #region AddAsync
        Task<RatingModel> AddAsync(RatingModel _Oder);
        #endregion

        #region RemoveAsync
        Task<RatingModel> RemoveAsync(Guid OderId);
        #endregion

        #region UpdaAsync
        Task<RatingModel> UpdaAsync(RatingModel OderId);
        #endregion
    }
}
