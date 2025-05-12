using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Product.Model;
public record ProductDTO
    (string Name,decimal Weight,int Quantity);
public record  UpdateProductDTO
    (int Id,string Name,decimal Weight,int Quantity);