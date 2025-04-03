using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Core.Abstraction;

public interface ICityService
{
    Task<List<getCtiyDto>> getAllCityAsync();
    Task<bool> addCityAsync(addCityDto Addcitydto);
    Task<getCtiyDto> getCityByIdAsync(int id);
    Task<List<getCtiyDto>> getCitybyGovaernorateName(int regoinId);
}
