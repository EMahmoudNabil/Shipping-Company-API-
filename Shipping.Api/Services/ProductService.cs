using AutoMapper;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product, int> _genericRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product, int> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<ADDProductDto> CreateProductAsync(ADDProductDto ProductDto)
        {
            if (ProductDto == null)
            {
                return null;
            }
            Product product = _mapper.Map<Product>(ProductDto);
            await _genericRepository.AddAsync(product);
            return _mapper.Map<ADDProductDto>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            Product product = await _genericRepository.GetByIdAsync(id);
            if (product == null)
            {
                return;
            }
            await _genericRepository.DeleteAsync(product);
        }


        public async Task<List<ADDProductDto>> GetAllProductsAsync()
        {

           List< Product> product = await _genericRepository.GetAllAsync();
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<List<ADDProductDto>>(product);

        }

        public async Task<ADDProductDto> GetProductByIdAsync(int id)
        {
          Product product = await _genericRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            ADDProductDto productDto = _mapper.Map<ADDProductDto>(product);
            return productDto;
        }

        public async Task UpdateProductAsync(UpdateProductDto ProductDto)
        {
          Product product = _mapper.Map<Product>(ProductDto);
            await _genericRepository.UpdateAsync(product);
        }
    }
}
