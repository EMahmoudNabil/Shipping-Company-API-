using System.ComponentModel;

namespace Shipping.Api.Infrastructure.Data;

public record getCtiyDto
{
    public string Govaernorate { get; set; }
    public string Name { get; set; }

    [DisplayName("Status")]
    public bool IsDeleted { get; set; }
    public decimal StandardShippingCost { get; set; }
    public decimal pickupShippingCost { get; set; }
};
public record addCityDto
{
    public int RegionId { get; set; }
    public string Name { get; set; } 
    public decimal StandardShippingCost { get; set; }
    public decimal pickupShippingCost { get; set; }
};
