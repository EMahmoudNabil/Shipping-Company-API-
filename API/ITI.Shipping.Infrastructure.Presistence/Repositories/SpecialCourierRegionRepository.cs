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
// This Is A SpecialCourierRegion Repository Class That Implements The ISpecialCourierRegionRepository Interface
public class SpecialCourierRegionRepository:GenericRepository<SpecialCourierRegion,int>, ISpecialCourierRegionRepository
{
    private readonly ApplicationContext _applicationContext;
    public SpecialCourierRegionRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task AddRangeAsync(IEnumerable<SpecialCourierRegion> entities)
    {
        await _applicationContext.SpecialCourierRegion.AddRangeAsync(entities);
        await _applicationContext.SaveChangesAsync();
    }
}