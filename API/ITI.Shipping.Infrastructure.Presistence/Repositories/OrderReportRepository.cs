using ITI.Shipping.Core.Domin.Entities;
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
// This Is OrderReport Repository Class That Implements The IOrderReportRepository Interface
public class OrderReportRepository:GenericRepository<OrderReport,int>, IOrderReportRepository
{
    private readonly ApplicationContext _Context;

    public OrderReportRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
        _Context = applicationContext;
    }
    public async Task<IEnumerable<OrderReport>> GetOrderReportByPramter(OrderReportPramter pramter)
    {
       
        if(pramter.OrderStatus == null && pramter.PageSize == null && pramter.PageNumber == null 
            && pramter.DateFrom == null && pramter.DateTo == null)
        {
            return await _context.OrderReports.ToListAsync();

        }
        else
        {
            IQueryable<OrderReport> orderreportes = _Context.OrderReports;
            if(pramter.OrderStatus != null)
            {
                orderreportes = orderreportes.Where(x => x.Order!.Status == pramter.OrderStatus);
            }
            if(pramter.DateFrom != null)
            {
                orderreportes = orderreportes.Where(x => x.ReportDate >= pramter.DateFrom);
            }
            if(pramter.DateTo != null)
            {
                orderreportes = orderreportes.Where(x => x.ReportDate <= pramter.DateTo);
            }
            if(pramter.PageSize != null && pramter.PageNumber != null)
            {
                orderreportes = orderreportes
                    .Skip((pramter.PageNumber.Value - 1) * pramter.PageSize.Value)
                    .Take(pramter.PageSize.Value);                   
            }
           return await orderreportes.ToListAsync();
        }     
    }
}
