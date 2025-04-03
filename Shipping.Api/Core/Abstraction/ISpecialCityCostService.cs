using Shipping.Api.Core.Domain.Models;

namespace Shipping.Api.Core.Abstraction;

public interface ISpecialCityCostService
{
    Task AddRangeAsync(IEnumerable<SpecialCityCost> entities);
}
