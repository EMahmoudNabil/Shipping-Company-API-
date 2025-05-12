using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.OrderReport
{
    public interface IOrderReportService
    {
        //Task<IEnumerable<OrderReportToShowDTO>> GetAllOrderReportAsync(Pramter pramter);
        Task<IEnumerable<OrderReportToShowDTO>> GetAllOrderReportAsync(OrderReportPramter pramter);
        Task<OrderReportDTO> GetOrderReportAsync(int id);
        Task AddAsync(OrderReportDTO DTO);
        Task UpdateAsync(OrderReportDTO DTO);
        Task DeleteAsync(int id);
    }
}
