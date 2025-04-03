using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Services;

public class SpecialCourierRegionService(ShippingContext context):ISpecialCourierRegionService
{
    private readonly ShippingContext _context = context;

    public async Task AddRangeAsync(IEnumerable<SpecialCourierRegion> entities)
    {
        await _context.GetSpecialCourierRegions.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}
