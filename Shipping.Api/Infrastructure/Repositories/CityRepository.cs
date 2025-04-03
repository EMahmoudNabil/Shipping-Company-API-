using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Infrastructure.Repositories;

public class CityRepository:GenericRepository<CitySetting,int>, ICityRepository
{
    
    public CityRepository(ShippingContext context) : base(context)
    {
      
    }
    public async Task<List<CitySetting>> getCitybyGovaernorateName(int regoinId)
    {
        var city =  _context.CitySettings.Where(C=>C.RegionId==regoinId).ToListAsync();
        if(city == null)
        {
            return null!;
        }
        return await  city;
    }
}