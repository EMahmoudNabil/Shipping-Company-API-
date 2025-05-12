using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using System.Text.Json.Serialization;
namespace ITI.Shipping.Core.Application.Abstraction.Order.Model
{
    public record OrderWithProductsDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Notes { get; set; }
        public required string Status { get; set; }
        public string? Branch { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public decimal orderCost { get; set; }
        public string CustomerInfo { get; set; } = string.Empty;

        // Change these properties:
        public string MerchantId { get; set; } = string.Empty;  // For the ID
        public string MerchantName { get; set; } = string.Empty; // For the display name

        public string CourierId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
    public record addOrderDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public OrderType OrderTypes { get; set; }
        public bool IsOutOfCityShipping { get; set; }
        public int ShippingId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public new int? Branch { get; set; }
        public new int? Region { get; set; }
        [JsonIgnore]
        public  decimal ShippingCost { get; set; }
        public new int City { get; set; }
        public decimal TotalWeight { get; set; }
        public string MerchantId { get; set; } = string.Empty;  // For the ID
        public string MerchantName { get; set; } = string.Empty;

        [Range(0.01,double.MaxValue,ErrorMessage = "Order cost must be greater than zero")]
        public decimal OrderCost { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone1 { get; set; } = string.Empty;
        public string CustomerPhone2 { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        [MinLength(1,ErrorMessage = "At least one product is required")]
        public List<ProductDTO> Products { get; set; } = new();
        [JsonIgnore]
        public OrderStatus status { get; set; }
    }
    public record updateOrderDto:addOrderDto
    {
        public int Id { get; set; }
    }

}
