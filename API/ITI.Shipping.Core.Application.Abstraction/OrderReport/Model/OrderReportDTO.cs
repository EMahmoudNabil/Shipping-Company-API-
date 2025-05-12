using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ITI.Shipping.Core.Domin.Entities_Helper;
namespace ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
public class OrderReportDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public string ReportDetails { get; set; } = string.Empty;
    public DateTime ReportDate { get; set; } = DateTime.Now;
    //----------- Order  ---------------------------------
    public int? OrderId { get; set; }
}
public record OrderReportToShowDTO
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    [JsonIgnore]
    public string? MerchantName { get; set; }
    //[JsonIgnore]
    public string? MerchantId { get; set; }
    [JsonIgnore]
    public string? CourierId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhone1 { get; set; }
    public string? RegionName { get; set; }
    public string? BranchName { get; set; }
    public decimal OrderCost { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal AmountReceived { get; set; }
    public decimal ShippingCostPaid { get; set; }
    public decimal? CompanyValue { get; set; }
    public DateTime ReportDate { get; set; } = DateTime.Now;
    [JsonIgnore]
    public string PaymentType { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; } 
}
