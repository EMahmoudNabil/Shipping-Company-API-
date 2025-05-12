using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.SpecialCityCost
{
    public interface ISpecialCityCostService
    {
        Task<IEnumerable<SpecialCityCostDTO>> GetAllSpecialCityCostAsync(Pramter pramter);
        Task<SpecialCityCostDTO> GetSpecialCityCostAsync(int id);
        Task AddAsync(SpecialCityCostDTO DTO);
        Task UpdateAsync(SpecialCityCostDTO DTO);
        Task DeleteAsync(int id);
    }
}
