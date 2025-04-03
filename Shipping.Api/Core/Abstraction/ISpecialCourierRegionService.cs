using Shipping.Api.Core.Domain.Models;

namespace Shipping.Api.Core.Abstraction;

public interface ISpecialCourierRegionService
{
    Task AddRangeAsync(IEnumerable<SpecialCourierRegion> entities);
}
