using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Merchant.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MerchantController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public MerchantController(IServiceManager serviceManager)
    {
       _serviceManager = serviceManager;
    }
    [HttpGet("GetMerchant")]
    [HasPermission(Permissions.ViewMerchants)]
    public async Task<ActionResult<IEnumerable<MerchantDTO>>> GetAllMerchant()
    {
        var merchants = await _serviceManager.merchantService.GetMerchantAsync();
        return Ok(merchants);
    }
}
