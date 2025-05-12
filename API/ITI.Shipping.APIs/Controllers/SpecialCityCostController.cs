using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SpecialCityCostController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SpecialCityCostController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet] // Get : /api/SpecialCityCost
    [HasPermission(Permissions.ViewCities)]
    public async Task<ActionResult<IEnumerable<SpecialCityCostDTO>>> GetAllSpecialCityCost([FromQuery] Pramter pramter)
    {
        var AllSpecialCityCost = await _serviceManager.specialCityCostService.GetAllSpecialCityCostAsync(pramter);
        return Ok(AllSpecialCityCost);
    }
    [HttpGet("{id}")] // Get : /api/SpecialCityCost/id
    [HasPermission(Permissions.ViewCities)]
    public async Task <ActionResult<SpecialCityCostDTO>> GetSpecialCityCost(int id)
    {
        var SpecialCityCost = await _serviceManager.specialCityCostService.GetSpecialCityCostAsync(id);
        return Ok(SpecialCityCost);
    }
    [HttpPost] // Post : /api/SpecialCityCost
    [HasPermission(Permissions.AddCities)]
    public async Task<ActionResult<SpecialCityCostDTO>> AddSpecialCityCost(SpecialCityCostDTO DTO)
    {
        if(DTO == null)
            return BadRequest("Invalid SpecialCityCost data");
        await _serviceManager.specialCityCostService.AddAsync(DTO);
        return Ok();
    }
    [HttpPut("{id}")] // Put : /api/SpecialCityCost/id 
    [HasPermission(Permissions.UpdateCities)]
    public async Task<ActionResult> UpdateSpecialCityCost(int id,[FromBody] SpecialCityCostDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid SpecialCityCost data.");
        try
        {
            await _serviceManager.specialCityCostService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/SpecialCityCost/id
    [HasPermission(Permissions.DeleteCities)]
    public async Task<ActionResult> DeleteSpecialCityCost(int id)
    {
        try
        {
            await _serviceManager.specialCityCostService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
