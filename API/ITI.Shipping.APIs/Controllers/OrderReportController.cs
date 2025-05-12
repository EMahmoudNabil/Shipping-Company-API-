using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderReportController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public OrderReportController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    //[HttpGet] // Get : /api/OrderReport
    //[HasPermission(Permissions.ViewOrderReports)]
    //public async Task<ActionResult<IEnumerable<OrderReportToShowDTO>>> GetAllOrderReport([FromQuery] Pramter pramter)
    //{
    //    var AllOrderReport = await _serviceManager.orderReportService.GetAllOrderReportAsync(pramter);
    //    return Ok(AllOrderReport);
    //}

    [HttpGet("GetAllByPramter")] // Get : /api/OrderReport/GetAllByPramter
    [HasPermission(Permissions.ViewOrderReports)]
    public async Task<ActionResult<IEnumerable<OrderReportToShowDTO>>> GetAllByPramter([FromQuery] OrderReportPramter pramter)
    {
        var AllOrderReport = await _serviceManager.orderReportService.GetAllOrderReportAsync(pramter);
        return Ok(AllOrderReport);
    }



    [HttpGet("{id}")] // Get : /api/OrderReport/id
    [HasPermission(Permissions.ViewOrderReports)]
    public async Task<ActionResult<OrderReportDTO>> GetOrderReport(int id)
    {
        var OrderReport = await _serviceManager.orderReportService.GetOrderReportAsync(id);
        if(OrderReport == null)
        {
            return NotFound();
        }
        return Ok(OrderReport);
    }
    //[HttpPost] // Post : /api/OrderReport
    //[HasPermission(Permissions.AddOrderReports)]
    //public async Task<ActionResult<OrderReportDTO>> AddOrderReport(OrderReportDTO DTO)
    //{
    //    if(DTO == null)
    //        return BadRequest("Invalid OrderReport data");
    //    await _serviceManager.orderReportService.AddAsync(DTO);
    //    return Ok();
    //}
    [HttpPut("{id}")] // Put : /api/OrderReport/id
    [HasPermission(Permissions.UpdateOrderReports)]
    public async Task<ActionResult> UpdateOrderReport(int id,[FromBody] OrderReportDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid OrderReport data.");
        try
        {
            await _serviceManager.orderReportService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/OrderReport/id
    [HasPermission(Permissions.DeleteOrderReports)]
    public async Task<ActionResult> DeleteOrderReport(int id) 
    {
        try
        {
            await _serviceManager.orderReportService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
