using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Courier.DTO;
public record CourierDTO
{
    public string CourierId { get; set; } = string.Empty;
    public string CourierName { get; set; } = string.Empty;
}
