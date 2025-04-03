using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction
{
    public interface IProductService
    {
        Task<List<ADDProductDto>> GetAllProductsAsync();
        Task<ADDProductDto> GetProductByIdAsync(int id);
        Task<ADDProductDto> CreateProductAsync(ADDProductDto ProductDto);
        Task UpdateProductAsync(UpdateProductDto ProductDto);
        Task DeleteProductAsync(int id);


    }
}
