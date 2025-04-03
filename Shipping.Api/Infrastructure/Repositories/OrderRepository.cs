using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Infrastructure.Repositories;

public class OrderRepository:GenericRepository<Order,int>, IOrderRepository
{
    public OrderRepository(ShippingContext context) : base(context)
    {
    }
    public async Task<List<Order>> GetOrdersByStatus(OrderStatus status) {
        var orders= _context.Orders.Where(x => x.Status == status).ToListAsync();
        if(orders == null)
        {
            return null!;
        }
        return await orders;

    }
}
