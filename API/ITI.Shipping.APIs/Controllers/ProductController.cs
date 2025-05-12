using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet] // Get : /api/Product
    [HasPermission(Permissions.ViewOrders)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] Pramter pramter)
    {
        var Products = await _serviceManager.productService.GetProductsAsync(pramter);
        return Ok(Products);
    }
    [HttpGet("{id}")] // Get : /api/Product/id
    [HasPermission(Permissions.ViewOrders)]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var Product = await _serviceManager.productService.GetProductAsync(id);
        if(Product == null)
            return NotFound($"Product with id {id} not found.");
        return Ok(Product);
    }
    [HttpPost] // Post : /api/Product
    [HasPermission(Permissions.AddOrders)]
    public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO DTO)
    {
        if(DTO == null)
            return BadRequest("Invalid Product data");
        await _serviceManager.productService.AddAsync(DTO);
        return Ok();
    }
    [HttpPut("{id}")] // Put : /api/Product/id
    [HasPermission(Permissions.UpdateOrders)]
    public async Task<ActionResult> UpdateProduct(int id,[FromBody] UpdateProductDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid Product data.");
        try
        {
            await _serviceManager.productService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/Product/id
    [HasPermission(Permissions.DeleteOrders)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        try
        {
            await _serviceManager.productService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
