using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion
{
    public interface ISpecialCourierRegionService
    {
        Task<IEnumerable<SpecialCourierRegionDTO>> GetSpecialCourierRegionsAsync(Pramter pramter);
        Task<SpecialCourierRegionDTO> GetSpecialCourierRegionAsync(int id);
        Task AddAsync(SpecialCourierRegionDTO DTO);
        Task UpdateAsync(SpecialCourierRegionDTO DTO);
        Task DeleteAsync(int id);
    }
}