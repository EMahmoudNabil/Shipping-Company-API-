using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierReportController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CourierReportController(IServiceManager serviceManager)
        {
           _serviceManager = serviceManager;
        }
        [HttpGet] // Get : /api/CourierReport
        [HasPermission(Permissions.ViewCouriers)]
        public async Task<ActionResult<IEnumerable<GetAllCourierOrderCountDto>>> GetAllReports([FromQuery] Pramter pramter)
        {
            var CourierReports = await _serviceManager.CourierReportService.GetAllCourierReportsAsync(pramter);
            return Ok(CourierReports);
        }
        [HttpGet("{id}")] // Get : /api/CourierReport/id
        [HasPermission(Permissions.ViewCouriers)]
        public async Task<ActionResult<CourierReportDto>> GetBranch(int id ,[FromQuery] Pramter pramter)
        {
            var CourierReport = await _serviceManager.CourierReportService.GetCourierReportByIdAsync(id ,pramter);
            if(CourierReport == null)
            {
                return NotFound();
            }
            return Ok(CourierReport);
        }

    }
}
