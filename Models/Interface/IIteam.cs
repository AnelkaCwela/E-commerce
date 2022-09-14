using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IIteam
    {
        //#region TaBAsync
        //Task<IEnumerable<IteamModel>> TaBAsync();
        //#endregion
        #region TabAsync
        Task<IEnumerable<IteamModel>> GetListItemByCategoryAsync(Guid id);
        #endregion
        #region TabAsync
        Task<IEnumerable<IteamModel>> TabAsync();
        #endregion
        #region UpdatQuaAsync
        Task<IteamModel> UpdatQuaAsync(IteamModel IteamId);
        #endregion
        #region UpdatStocksync
        Task<bool> UpdatStocksync(Guid id ,bool stock);
        #endregion
        #region GetByIdAsync
        Task<IteamModel> GetByIdAsync(Guid IteamId);
        #endregion

        

        #region AddAsync
        Task<IteamModel> AddAsync(IteamModel _Iteam);
        #endregion
        #region RemoveAsync
        Task<IteamModel> RemoveAsync(Guid IteamId);
        #endregion
        #region UpdatAsync
        Task<IteamModel> UpdatAsync(IteamModel IteamId);
        #endregion
        #region TabGroupAsync
        Task<IEnumerable<IteamModel>> TabGroupAsync(Guid SupplierId);
      #endregion
    }
}
