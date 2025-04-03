using Shipping.Api.Core.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Api.Infrastructure.Dtos;


    public record OrderWithProductsDto
{

    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public string? Notes { get; set; }
       
    public required  string Status { get; set; }

    // Ids From User
    public string? Branch { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }


    //customer info

      public string CustomerInfo { get; set; } = string.Empty;
    // merchant id
    public virtual string  MerchantName { get; set; } = string.Empty;

    //[Required]
    //public string CourierId { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
       
    }

public record addOrderDto
{

    public bool IsOutOfCityShipping { get; set; }
    public string? ShippingType { get; set; }
    public PaymentType? PaymentType { get; set; }
  
    // Ids From User
    public new int? Branch { get; set; }
    public new int? Region { get; set; }

    public new int? City { get; set; }
    public decimal TotalWeight { get; set; }
    public string MerchantName { get; set; } 


    [Range(0.01,double.MaxValue,ErrorMessage = "Order cost must be greater than zero")]
    public decimal OrderCost { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    public string CustomerPhone1 { get; set; } = string.Empty;
    public string CustomerPhone2 { get; set; } = string.Empty;

    public string CustomerAddress { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    //list of products
    [MinLength(1,ErrorMessage = "At least one product is required")]


    public List<ProductDto> Products { get; set; } = new();
}
public record updateOrderDto:addOrderDto
{
    public  int Id { get; set; }
}

