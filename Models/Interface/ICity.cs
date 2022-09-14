using System;
using System.Collections.Generic;
using OurShop.Models.DataModel;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop.Models.Interface
{
    public interface ICity
    {
        #region TabAsync
        Task<IEnumerable<CityModel>> TabAsync();
        #endregion

        //#region GetByIdAsync
        //Task<CityModel> GetByIdAsync(Guid CityId);
        //#endregion

        #region AddAsync
        Task<CityModel> AddAsync(CityModel _City);
        #endregion
    }
}
