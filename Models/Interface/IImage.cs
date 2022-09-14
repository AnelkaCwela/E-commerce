using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IImage
    {

        #region GetAsync
        Task<IEnumerable<ImageModel>> GetByIdAsync(Guid ImageId);
        #endregion

        #region TabAsync
        Task<IEnumerable<ImageModel>> TabAsync();
        #endregion
        #region TabAsync
        Task<IEnumerable<ImageModel>> GetListByItemIdsync(Guid id);
        #endregion

        #region RemoveAsync
        Task<ImageModel> RemoveAsync(Guid id);
        #endregion
        #region AddAsync
        Task<ImageModel> AddAsync(ImageModel _Imagel);
        #endregion
    }
}
