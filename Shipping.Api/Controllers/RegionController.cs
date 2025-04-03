using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Services;

namespace Shipping.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionController:ControllerBase
{
    private readonly IRegionService _regionService;
    public RegionController(IRegionService _regionService)
    {
        this._regionService = _regionService;

    }
    [HttpGet]
    public async Task<IActionResult> GetAllRegions()
    {
        try
        {
            if(await _regionService.GetAllRegionsAsync() == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));

            }
            return Ok(await _regionService.GetAllRegionsAsync());
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));

        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRegionById(int id)
    {
        try
        {
            if(await _regionService.GetRegionByIdAsync(id) == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));

            }
            return Ok(await _regionService.GetRegionByIdAsync(id));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));

        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateRegion(RegionDto region)
    {
        try
        {
            if(region == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));

            }
            await _regionService.CreateRegionAsync(region);
            return Created();
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));

        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegion(int id,RegionDto region)
    {
        try
        { 
        if(id != region.Id)
        {
            return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"ID Not Match"));

        }
        await _regionService.UpdateRegionAsync(region);
            return Ok(new ResponseAPI(StatusCodes.Status202Accepted));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));

        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(int id)
    {
        try
        {
        if(await _regionService.GetRegionByIdAsync(id) == null)
        {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"ID Not Match"));

            }
            await _regionService.DeleteRegionAsync(id);
            return Ok(new ResponseAPI(StatusCodes.Status200OK));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));

        }
    }
}
