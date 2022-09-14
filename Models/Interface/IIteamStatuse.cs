using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;

namespace OurShop.Models.Interface
{
    public interface IIteamStatuse
    {
        #region TabAsync
        Task<IEnumerable<IteamStatuseModel>> TabAsync();

           #endregion

        #region AddAsync
        Task<IteamStatuseModel> AddAsync(IteamStatuseModel _Color);
        #endregion
        
    }
}
