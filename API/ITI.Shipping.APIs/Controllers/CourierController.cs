using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Courier.DTO;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CourierController:ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CourierController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourierDTO>>> GetAllCouriers([FromQuery] int branchId)
    {
        var couriers = await _serviceManager.courierService.GetCourierByBranch(branchId);
        return Ok(couriers);
    }

}
