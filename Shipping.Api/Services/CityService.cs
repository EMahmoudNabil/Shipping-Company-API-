using AutoMapper;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Services;

public class CityService:ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _map;
    public CityService(IMapper map,ICityRepository cityRepository)
    {
       
        _map = map;
        _cityRepository = cityRepository;
    }
    public async Task<List<getCtiyDto>> getAllCityAsync()
    {
        var Cities = await _cityRepository.GetAllAsync();
        if(Cities == null)
        {
            return null!;
        }
        return _map.Map<List<getCtiyDto>>(Cities);

    }
    public async Task<getCtiyDto> getCityByIdAsync(int id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if(city == null)
        {
            return null!;
        }
        return _map.Map<getCtiyDto>(city);
    }
    public async Task<List<getCtiyDto>> getCitybyGovaernorateName(int regoinId) { 
    
    List<CitySetting> city = await _cityRepository.getCitybyGovaernorateName(regoinId);
        if(city == null)
        {
            return null!;
        }
        return _map.Map<List<getCtiyDto>>(city);

    }

    public async Task<bool> addCityAsync(addCityDto Addcitydto)
    {
        if(Addcitydto == null)
        {
            return false;
        }
        var city = _map.Map<CitySetting>(Addcitydto);
        await _cityRepository.AddAsync(city);
        return true;
    }
   
}
