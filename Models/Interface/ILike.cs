using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

using OurShop.Models.Interface;

namespace OurShop.Models.Interface
{
    public interface ILike
    {
        #region TabAsync
        Task<IQueryable<LikeModel>> TabAsync();

        #endregion

        #region GetByIdAsync
        Task<LikeModel> GetByIdAsync(Guid LikeId);
        #endregion

        #region AddAsync
        Task<LikeModel> AddAsync(LikeModel _Like);
        #endregion

        #region RemoveAsync
        Task<LikeModel> RemoveAsync(Guid LikeId);
        #endregion

    }
}
