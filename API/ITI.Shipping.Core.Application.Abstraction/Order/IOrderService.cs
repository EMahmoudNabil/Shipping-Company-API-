using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderWithProductsDto>> GetOrdersAsync(Pramter pramter);
        Task<OrderWithProductsDto> GetOrderAsync(int id);
        Task<IEnumerable<OrderWithProductsDto>> GetOrdersByStatus(OrderStatus status,Pramter pramter);
        Task<IEnumerable<OrderWithProductsDto>> GetAllWatingOrder(Pramter pramter);
         Task ChangeOrderStatus(int id,OrderStatus orderStatus);
        Task ChangeOrderStatusToPending(int id);
        Task ChangeOrderStatusToDeclined(int id);
        Task AssignOrderToCourier (int OrderId,string courierId);
        Task AddAsync(addOrderDto DTO);
        Task UpdateAsync(updateOrderDto DTO);
        Task DeleteAsync(int id);
        Task<updateOrderDto> GetOrderForUpdateAsync(int id);
    }
}
