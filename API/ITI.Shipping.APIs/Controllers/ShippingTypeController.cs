using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShippingTypeController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ShippingTypeController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet] // Get : /api/ShippingType
    [HasPermission(Permissions.ViewSettings)]
    public async Task<ActionResult<IEnumerable<ShippingTypeDTO>>> GetAllShippingType([FromQuery] Pramter pramter)
    {
        var allShippingType =await _serviceManager.shippingTypeService.GetAllShippingTypeAsync(pramter);
        return Ok(allShippingType);
    }
    [HttpGet("{id}")] // Get : /api/ShippingType/id
    [HasPermission(Permissions.ViewSettings)]
    public async Task<ActionResult<ShippingTypeDTO>> GetShippingType(int id)
    {
        var ShippingType = await _serviceManager.shippingTypeService.GetShippingTypeAsync(id);
        return Ok(ShippingType);
    }
    [HttpPost] // Post : /api/ShippingType
    [HasPermission(Permissions.AddSettings)]
    public async Task<ActionResult<ShippingTypeDTO>> AddShippingType(ShippingTypeDTO DTO)
    {
        if(DTO == null)
            return BadRequest("Invalid ShippingType data");
        await _serviceManager.shippingTypeService.AddAsync(DTO);
        return Ok();
    }
    [HttpPut("{id}")] // Put : /api/ShippingType/id
    [HasPermission(Permissions.UpdateSettings)]
    public async Task<ActionResult<ShippingTypeDTO>> UpdateShippingType(int id,[FromBody] ShippingTypeDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid ShippingType data");
        try
        {
            await _serviceManager.shippingTypeService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/ReShippingTypegion/id
    [HasPermission(Permissions.DeleteSettings)]
    public async Task<ActionResult> DeleteShippingType(int id)
    {
        try
        {
            await _serviceManager.shippingTypeService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}
