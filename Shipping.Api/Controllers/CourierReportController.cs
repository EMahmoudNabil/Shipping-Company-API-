using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Services;

namespace Shipping.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CourierReportController:ControllerBase
{
    private readonly ICourierReportsService _courierReportsService;
    public CourierReportController(ICourierReportsService _courierReportsService)
    {
        this._courierReportsService = _courierReportsService;

    }
    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        try
        {
            if(await _courierReportsService.GetAllCourierReportsAsync() == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
            }
            return Ok(await _courierReportsService.GetAllCourierReportsAsync());
        } catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
    [HttpGet("{id}")]
        public async Task<IActionResult> GetReports(int id)
        {
        try
        {

            if(await _courierReportsService.GetCourierReportByIdAsync(id) == null)
            {
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));


            }

            return Ok(await _courierReportsService.GetCourierReportByIdAsync(id));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }

    
}
