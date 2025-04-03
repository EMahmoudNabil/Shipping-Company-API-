using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;

namespace Shipping.Api.Core.Abstraction;

public interface IOrderRepository:IGenericRepository<Order,int>
{
    Task<List<Order>> GetOrdersByStatus(OrderStatus status);
}
