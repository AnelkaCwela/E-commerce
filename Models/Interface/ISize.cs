using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface ISize
    {
        #region TabAsync
        Task<IEnumerable<SizeModel>> TabAsync();

        #endregion

        #region GetByIdAsync
        Task<SizeModel> GetByIdAsync(Guid SizeId);
        #endregion

        #region AddAsync
        Task<SizeModel> AddAsync(SizeModel _Size);
        #endregion

        #region RemoveAsync
        Task<SizeModel> RemoveAsync(Guid SizeId);
        #endregion

        #region UpdaAsync
        Task<SizeModel> UpdaAsync(SizeModel SizeId);
        #endregion
    }
}
