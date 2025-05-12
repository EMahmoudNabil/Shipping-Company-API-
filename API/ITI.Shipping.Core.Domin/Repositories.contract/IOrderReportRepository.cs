using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Repositories.contract;
// This Is OrderReport Repository Interface That Inherits From The Generic Repository Interface
public interface IOrderReportRepository:IGenericRepository<OrderReport,int>
{
    // This Is A Method That Get All OrderReport By Pramter (Pagination , Status)
    Task<IEnumerable<OrderReport>> GetOrderReportByPramter(OrderReportPramter pramter);
}