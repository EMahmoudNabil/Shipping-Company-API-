using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Services;

public class SpecialCityCostService(ShippingContext context):IGenericRepository<SpecialCityCost,string>, ISpecialCityCostService
{
    private readonly ShippingContext _context = context;

    public Task AddAsync(SpecialCityCost entity)
    {
        throw new NotImplementedException();
    }

    public async Task AddRangeAsync(IEnumerable<SpecialCityCost> entities)
    {
        await _context.SpecialCityCosts.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(SpecialCityCost entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<SpecialCityCost>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SpecialCityCost?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SpecialCityCost entity)
    {
        throw new NotImplementedException();
    }
}
