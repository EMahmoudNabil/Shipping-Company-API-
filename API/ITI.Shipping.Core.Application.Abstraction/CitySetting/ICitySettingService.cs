using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.CitySetting
{
    public interface ICitySettingService
    {
        Task<IEnumerable<CitySettingDTO>> GetCitySettingsAsync(Pramter pramter);
        Task<CitySettingDTO> GetCitySettingAsync(int id);
        Task AddAsync(CitySettingToAddDTO DTO);
        Task UpdateAsync(CitySettingToUpdateDTO DTO);
        Task DeleteAsync(int id);
        Task<IEnumerable<CitySettingDTO>> GetCityByGovernorateName(int regionId);
    }
}
