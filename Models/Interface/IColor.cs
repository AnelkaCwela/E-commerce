using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OurShop.Models.DataModel;
namespace OurShop.Models.Interface
{
    public interface IColor
    {
        #region TabAsync
        Task<IEnumerable<ColorModel>> TabAsync();
        #endregion

        //#region GetByIdAsync

        //Task<ColorModel> GetByIdAsync(Guid ColorId);
        //#endregion

        #region AddAsync
        Task<ColorModel> AddAsync(ColorModel _Color);
        #endregion
        //ColorModel RemoveColor(int ColorId);
        //ColorModel UpdatColor(int ColorId);
    }
}
