

using Shipping.Api.Core.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Api.Core.Domain.Models;

    public class Order
    {
        public int Id { get; set; }
        [Required]
        public decimal TotalWeight { get; set; }
        [Required]
        public decimal OrderCost { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsOutOfCityShipping { get; set; } = false;
        //----------- Enum OrderStatus---------------------------------
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        //----------- Ids From User ---------------------------------
        public string MerchantId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string CourierId { get; set; } = string.Empty;
        //------------- ICollection From CourierReport ------------------------------
        public virtual ICollection<CourierReport> CouriersReport { get; set; } = [];
        //----------- Obj From Branch and ForeignKey BranchId ---------------------------------
        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        //----------- Obj From Region and ForeignKey RegionId ---------------------------------
        [ForeignKey(nameof(Region))]
        public int? RegionId { get; set; }
        public virtual Region? Region { get; set; }
        //----------- Obj From CitySetting and ForeignKey CitySettingId ---------------------------------
        [ForeignKey(nameof(CitySetting))]
        public int? CitySettingId { get; set; }
        public virtual CitySetting? CitySetting { get; set; }
        //----------- Obj From ShippingType and ForeignKey ShippingTypeId ---------------------------------
        [ForeignKey(nameof(ShippingType))]
        public int? ShippingTypeId { get; set; }
        public virtual ShippingType? ShippingType { get; set; }
        //----------- Obj From PaymentType and ForeignKey PaymentTypeId ---------------------------------
        public  PaymentType? PaymentType { get; set; }
    //----------- ICollection From Product ---------------------------------
      public bool IsDeleted { get; set; } = false;
    public virtual ICollection<Product>? Products { get; set; }
        //----------- Customer Info ---------------------------------
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone1 { get; set; } = string.Empty;
        public string CustomerPhone2 { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
    }

