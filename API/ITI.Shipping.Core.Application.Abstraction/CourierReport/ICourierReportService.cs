using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.CourierReport
{
    public interface ICourierReportService
    {
        Task<IEnumerable<GetAllCourierOrderCountDto>> GetAllCourierReportsAsync(Pramter pramter);
        Task<CourierReportDto> GetCourierReportByIdAsync(int id ,Pramter pramter);
    }
}
