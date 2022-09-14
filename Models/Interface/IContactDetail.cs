using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface IContactDetail
    {
        #region TabAsync
        Task<IQueryable<ContactDetailModel>> TabAsync();
        #endregion

        #region GetByIdAsync
        Task<ContactDetailModel> GetByIdAsync(Guid UserId);
        #endregion
        #region GetByBradIdAsync
        Task<ContactDetailModel> GetByBradIdAsync(Guid UserId);
        #endregion
        #region AddAsync
        Task<ContactDetailModel> AddAsync(ContactDetailModel _UserModel);
        #endregion
        #region RemoveAsync
        Task<ContactDetailModel> RemoveAsync(Guid UserId);
        #endregion
        #region UpdatAsync
        Task<ContactDetailModel> UpdatAsync(ContactDetailModel _UserModel);
        #endregion
    }
}
