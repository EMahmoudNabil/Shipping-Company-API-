using ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.ShippingType
{
    public interface IShippingTypeService
    {
        Task<IEnumerable<ShippingTypeDTO>> GetAllShippingTypeAsync(Pramter pramter);
        Task<ShippingTypeDTO> GetShippingTypeAsync(int id);
        Task AddAsync(ShippingTypeDTO DTO);
        Task UpdateAsync(ShippingTypeDTO DTO);
        Task DeleteAsync(int id);
    }
}
