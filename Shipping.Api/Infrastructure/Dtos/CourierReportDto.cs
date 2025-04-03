using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using System.Text.Json.Serialization;

namespace Shipping.Api.Infrastructure.Dtos;

public class CourierReportDto
{
    //[JsonIgnore]
    //public string CourierId { get; set; }
    //[JsonIgnore]
    public required int OrderId { get; set; }

    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public required string CourierName { get; set; }
    public required string Area { get; set; }
    public required string ClientName { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerAddress { get; set; }
    public required string  CustomerPhone { get; set; }
    public required List<string> products { get; set; }
    public  string? Notes { get; set; }
    
    public required string orderStatus { get; set; }

    public decimal Amount { get; set; }



}
