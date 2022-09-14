using OurShop.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface IIteamDetail
    {
        #region TabAsync
        Task<IEnumerable<IteamDetailModel>> Tab();
        #endregion
        #region TabAsync
        Task<IEnumerable<IteamDetailModel>> GetListByItemIdsync(Guid id);
        #endregion
        #region GetByIdAsync

        Task<IteamDetailModel> CheckAsync(Guid Id, Guid ColorId, Guid SizeId);
        #endregion

        #region GetByIdAsync

        Task<IteamDetailModel> GetById(Guid OderId);
        #endregion

        #region UpdaItem

        Task<bool> UpdaItem(Guid id, int item, bool Stock);
        #endregion
        #region GetByItemId

        Task<IteamDetailModel> GetByItemId(Guid OderId);
        #endregion

        #region AddAsync
        Task<IteamDetailModel> Add(IteamDetailModel _Oder);
        #endregion

        #region RemoveAsync
        Task<IteamDetailModel> Remove(Guid OderId);
        #endregion
        //#region UpdaAsync
        //Task<IteamDetailModel> UpdaPrice(Guid id, decimal Price);
        //#endregion
        #region UpdaAsync
        Task<IteamDetailModel> Upda(IteamDetailModel OderId);
        #endregion

    }
}
