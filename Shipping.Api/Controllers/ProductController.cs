using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                if (await _productService.GetAllProductsAsync() == null)
                {
                    return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
                }
                return Ok(await _productService.GetAllProductsAsync());
            }
            catch
            {
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                {
                    return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
                }
                return Ok(await _productService.GetProductByIdAsync(id));
            }
            catch
            {
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ADDProductDto product)
        {
            try
            {
                if (await _productService.CreateProductAsync(product) == null)
                {
                    return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
                }
                return Ok(await _productService.CreateProductAsync(product));
            }
            catch
            {
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto product)
        {
            try
            {
                //validation on id
                if (product.Id == 0)
                {
                    return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
                }

                //validation on id
                if (id != product.Id)
                {
                    return NotFound(new ResponseAPI(StatusCodes.Status404NotFound, "ID Not Match"));
                }

                await _productService.UpdateProductAsync(product);
                return Ok(new ResponseAPI(StatusCodes.Status202Accepted));
            }
            catch
            {
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                {
                    return NotFound(new ResponseAPI(StatusCodes.Status404NotFound));
                }
                await _productService.DeleteProductAsync(id);
                return Ok(new ResponseAPI(StatusCodes.Status202Accepted));
            }
            catch
            {
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest));
            }
        }
    }
}
