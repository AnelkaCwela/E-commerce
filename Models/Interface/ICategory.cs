using System;
using System.Collections.Generic;
using System.Linq;
using OurShop.Models.DataModel;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface ICategory
    {
        #region TabAsync
        Task<IEnumerable<CategoryModel>> TabAsync();
        #endregion
        #region ListByIdAsync
        Task<IEnumerable<CategoryModel>> ListByIdAsync(Guid id);
        #endregion

        #region GetByIdAsync

        Task<CategoryModel> GetByIdAsync(Guid CategoryId);
        #endregion

        #region AddAsync
        Task<CategoryModel> AddAsync(CategoryModel _CategoryModel);
        #endregion

        #region RemoveAsync
        Task<CategoryModel> RemoveAsync(Guid CategoryId);
        #endregion

        #region UpdatAsync
        Task<CategoryModel> UpdatAsync(CategoryModel _CategoryModel);
        #endregion
    }
}
