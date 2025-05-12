using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Product;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.ProductServices
{
    internal class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get All Products
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync(Pramter pramter)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _unitOfWork.GetRepository<Product,int>().GetAllAsync(pramter));
        }
        // Get Product By Id
        public async Task<ProductDTO> GetProductAsync(int id)
        {
            return _mapper.Map<ProductDTO>(await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(id));
        }
        // Add Product  
        public async Task AddAsync(ProductDTO DTO)
        {
            await _unitOfWork.GetRepository<Product,int>().AddAsync(_mapper.Map<Product>(DTO));
        }
        // Update Product
        public async Task UpdateAsync(UpdateProductDTO DTO)
        {
            var ProductRepo = _unitOfWork.GetRepository<Product,int>();

            var existingProduct = await ProductRepo.GetByIdAsync(DTO.Id);
            if(existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {DTO.Id} not found.");

            _mapper.Map(DTO,existingProduct); // Update existing entity with DTO data

            ProductRepo.UpdateAsync(existingProduct);
            await _unitOfWork.CompleteAsync();
        }
        // Delete Product
        public async Task DeleteAsync(int id)
        {
            var ProductRepo = _unitOfWork.GetRepository<Product,int>();

            var existingProduct = await ProductRepo.GetByIdAsync(id);
            if(existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            await ProductRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
