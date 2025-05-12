using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync(Pramter pramter);
        Task<ProductDTO> GetProductAsync(int id);
        Task AddAsync(ProductDTO DTO);
        Task UpdateAsync(UpdateProductDTO DTO);
        Task DeleteAsync(int id);
    }
}
