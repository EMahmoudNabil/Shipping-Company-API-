using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.CitySetting.Models
{
    public class CitySettingDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public decimal StandardShippingCost { get; set; }
        public decimal pickupShippingCost { get; set; }
        public DateTime CreatedAt { get; set; }
        //----------- Region ---------------------------------
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
        //------------- List From User ------------------------------
        public List<string> UsersName { get; set; } = [];
        //------------- List From Order ------------------------------
        public List<decimal> OrdersCost { get; set; } = [];
        //------------- List From SpecialPickup ------------------------------
        public List<string> UsersThatHasSpecialCityCost { get; set; } = [];
    }
}
