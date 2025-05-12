using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Dashboard.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DashboardController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public DashboardController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet("EmpDashboard")]
    public async Task<ActionResult<EmpDashboardDTO>> GetDashboardData()
    {
        var dashboardData = await _serviceManager.dashboardService.GetDashboardOfEmployeeAsync();
        return Ok(dashboardData);
    }
    [HttpGet("MerchantDashboard")]
    public async Task<ActionResult<MerchantDashboardDTO>> GetMerchantDashboardData()
    {
        var dashboardData = await _serviceManager.dashboardService.GetDashboardDataForMerchantAsync();
        return Ok(dashboardData);
    }
}
