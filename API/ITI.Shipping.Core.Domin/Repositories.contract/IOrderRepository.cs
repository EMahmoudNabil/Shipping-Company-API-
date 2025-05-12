using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Repositories.contract;
// This Is A Order Repository Interface That Inherits From The Generic Repository Interface
public interface IOrderRepository:IGenericRepository<Order,int>
{
    // This Is A Method That Get All Orders By pramter (Pagination , Order-Status)
    Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status,Pramter pramter);
    Task<IEnumerable<Order>> GetAllWatingOrder(Pramter pramter);
}
