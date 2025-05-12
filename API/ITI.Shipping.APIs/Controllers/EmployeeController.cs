using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public EmployeeController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    [HasPermission(Permissions.ViewEmployees)]
    public async Task<ActionResult> GetAllEmployees([FromQuery] Pramter pramter)
    {
        var employees = await _serviceManager.employeeService.GetEmployeesAsync(pramter);
        return Ok(employees);
    }
}
