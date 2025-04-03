using System.ComponentModel.DataAnnotations;

namespace Shipping.Api.Infrastructure.Dtos;

public record ProductDto
    (string Name,decimal Weight,int Quantity);



public record ADDProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    public int OrderId { get; set; }
}

public record UpdateProductDto: ADDProductDto
{
    public int Id { get; set; }
}