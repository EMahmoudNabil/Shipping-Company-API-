using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
// This Is A Order Repository Class That Implements The IOrderRepository Interface
public class OrderRepository:GenericRepository<Order,int>, IOrderRepository
{
    private readonly ApplicationContext _Context;

    public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
        _Context = applicationContext;
    }
    public async Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status,Pramter pramter)
    {
        var orders = _Context.Orders.Where(x => x.Status == status);

        if(orders == null)
        {
            return null!;
        }
        if(pramter.PageSize != null && pramter.PageNumber != null)
        {
            return await orders
                .Skip((pramter.PageNumber.Value - 1) * pramter.PageSize.Value)
                .Take(pramter.PageSize.Value)
                .ToListAsync();
        }
        else
        {
            return await orders.ToListAsync();
        }
    }
    public async Task<IEnumerable<Order>> GetAllWatingOrder(Pramter pramter)
    {
        return await GetOrdersByStatus(OrderStatus.WaitingForConfirmation,pramter);
    }
}
