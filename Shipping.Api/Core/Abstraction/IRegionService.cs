using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction;

public interface IRegionService
{
    Task<List<RegionDto>> GetAllRegionsAsync();
    Task<RegionDto> GetRegionByIdAsync(int id);
    Task<RegionDto> CreateRegionAsync(RegionDto regionDto);
    Task UpdateRegionAsync(RegionDto regionDto);
    Task DeleteRegionAsync(int id);

}
