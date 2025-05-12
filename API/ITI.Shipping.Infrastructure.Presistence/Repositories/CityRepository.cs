using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
public class CityRepository:GenericRepository<CitySetting,int>, ICityRepository
{
    private readonly ApplicationContext _applicationContext;
    // This Is A City Repository Class That Implements The ICityRepository Interface
    public CityRepository(ApplicationContext applicationContext):base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
    // This Method Is Used To Get All Cities By Region Id
    public async Task<IEnumerable<CitySetting>> GetCityByGovernorateName(int regionId)
    {
        return await _applicationContext.CitySettings
        .Where(c => c.RegionId == regionId)
        .ToListAsync();
    }
}