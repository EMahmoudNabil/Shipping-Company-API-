namespace Shipping.Api.Infrastructure.Dtos;

public class GetAllCourierOrderCountDto
{
   public string CourierName { get; set; }

    public int OrdersCount { get; set; } = 0;
        
 };
