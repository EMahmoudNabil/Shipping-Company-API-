using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Services;

namespace Shipping.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController:ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("GetAllOrders")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        { 
        var orders = await _orderService.GetAllOrdersAsyncAndDeleteOrder();
        if(orders.Count == 0)
            return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"No orders found"));
            return Ok(orders);
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
    [HttpGet("GetOrder/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        { 
        var order = await _orderService.GetOrderByIdAsync(id);
        if(order is null)
            return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"Order not found"));

            return Ok(order);
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
    [HttpGet("GetAllOrdersByStatus/{status}")]
    public async Task<IActionResult> GetAllOrdersByStatus(OrderStatus status)
    {
        try
        {
            var orders = await _orderService.GetOrdersByStatus(status);
            if(orders.Count == 0)
                return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"No orders found"));
            return Ok(orders);
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] addOrderDto orderDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
        var result = await _orderService.CreateOrderAsync(orderDto);
         if (!result) return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,"Failed to create order"));

            return Created();
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateOrder([FromBody] updateOrderDto orderDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        { 
        var result = await _orderService.UpdateOrderAsync(orderDto);
        if(!result)
            return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"Order not found"));
            return NoContent();
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        { 
        var result = await _orderService.DeleteOrderAsync(id);
        if(!result)
            return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"Order not found or already deleted"));
            return Ok(new ResponseAPI(StatusCodes.Status200OK,"Order soft deleted successfully"));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }


    [HttpPut("restore/{id}")]
    public async Task<IActionResult> RestoreOrder(int id)
    {
        try
        {
        var result = await _orderService.RestoreOrderAsync(id);
        if(!result)
            return NotFound(new ResponseAPI(StatusCodes.Status404NotFound,"Order not found or not deleted"));

            return Ok(new ResponseAPI(StatusCodes.Status200OK,"Order restored successfully"));
        }
        catch
        {
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
        }
    }
}

