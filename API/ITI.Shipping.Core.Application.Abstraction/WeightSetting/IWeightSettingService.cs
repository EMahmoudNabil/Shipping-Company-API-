using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.WeightSetting
{
    public interface IWeightSettingService
    {
        Task<IEnumerable<WeightSettingDTO>> GetAllWeightSettingAsync(Pramter pramter);
        Task<WeightSettingDTO> GetWeightSettingAsync(int id);
        Task AddAsync(WeightSettingDTO DTO);
        Task UpdateAsync(WeightSettingDTO DTO);
        Task DeleteAsync(int id);
    }
}
