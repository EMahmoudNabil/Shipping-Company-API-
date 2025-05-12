using ITI.Shipping.Core.Application.Abstraction.Dashboard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Dashboard;
public interface IDashboardService
{
    Task<EmpDashboardDTO> GetDashboardOfEmployeeAsync();
    Task <MerchantDashboardDTO> GetDashboardDataForMerchantAsync();
}
