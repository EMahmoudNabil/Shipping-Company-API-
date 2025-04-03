using AutoMapper;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services;

public class RegionService:IRegionService
{
    private readonly IGenericRepository<Region,int> _genericRepository;
    private readonly IMapper _map;
    public RegionService(IGenericRepository<Region,int> genericRepository,IMapper _map)
    {
        this._genericRepository = genericRepository;
        this._map = _map;
    }
    public async Task<List<RegionDto>> GetAllRegionsAsync()
    {
        List<Region> regions = await _genericRepository.GetAllAsync();
        if(regions == null)
        {
            return null;
        }
        return _map.Map<List<RegionDto>>(regions);
    }
    public async Task<RegionDto> GetRegionByIdAsync(int id)
    {

        Region region = await _genericRepository.GetByIdAsync(id);
        if(region == null)
        {
            return null;
        }
        RegionDto regionDto = _map.Map<RegionDto>(region);
        return regionDto;
    }
    public async Task<RegionDto> CreateRegionAsync(RegionDto regionDto)
    {
        if(regionDto == null)
        {
            return null;
        }
        Region region = _map.Map<Region>(regionDto);
        await _genericRepository.AddAsync(region);
        return _map.Map<RegionDto>(region);
    }
    public async Task UpdateRegionAsync(RegionDto regionDto)
    {
        Region region = _map.Map<Region>(regionDto);
        await _genericRepository.UpdateAsync(region);
    }
    public async Task DeleteRegionAsync(int id)
    {
        Region region = await _genericRepository.GetByIdAsync(id);
        if(region == null)
        {
            return;
        }
        region.IsDeleted = true;
        await _genericRepository.UpdateAsync(region);
    }
}
