using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction;

public interface IOrderService
{
    Task<List<OrderWithProductsDto>> GetAllOrdersAsyncAndDeleteOrder();
    Task<List<OrderWithProductsDto>> GetOrdersByStatus(OrderStatus status);
    Task<OrderWithProductsDto?> GetOrderByIdAsync(int id);
    Task<bool> CreateOrderAsync(addOrderDto addorderDto);
    Task<bool> UpdateOrderAsync(updateOrderDto UpdateOrderDto);
    Task<bool> DeleteOrderAsync(int id);
    Task<bool> RestoreOrderAsync(int id);
}
