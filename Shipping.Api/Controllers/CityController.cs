using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CityController:ControllerBase
{
    private readonly ICityService _cityService;
    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }
    [HttpGet("GetAllCities")]
    public async Task<IActionResult> GetAllCityAsync()
    {
        try
        {
        var cities = await _cityService.getAllCityAsync();
        if(cities == null)
        {
            return NotFound( new ResponseAPI(StatusCodes.Status404NotFound));
            }
            return Ok(cities);
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCityByIdAsync(int id)
    {
        try
        {
            var city = await _cityService.getCityByIdAsync(id);
            if(city == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
            }
            return Ok(city);
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
    [HttpGet("GetCityByRegion/{regionId}")]
    public async Task<IActionResult> GetCityByRegionAsync(int regionId)
    {
        try
        {
            var cities = await _cityService.getCitybyGovaernorateName(regionId);
            if(cities == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
            }
            return Ok(cities);
        }
        catch(Exception ex)
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,ex.Message));
        }
    }
    [HttpPost("AddCity")]
    public async Task<IActionResult> AddCityAsync(addCityDto addCityDto)
    {
        try
        {
            if(await _cityService.addCityAsync(addCityDto) == false)
            {
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
            }
            return Ok(new ResponseAPI(StatusCodes.Status201Created));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
}
