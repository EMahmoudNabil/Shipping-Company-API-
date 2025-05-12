using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
public class ShippingTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public decimal BaseCost { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedAt { get; set; }
    //------------- List Of Order ------------------------------
    public List<int> OrdersId { get; set; } = [];
}
