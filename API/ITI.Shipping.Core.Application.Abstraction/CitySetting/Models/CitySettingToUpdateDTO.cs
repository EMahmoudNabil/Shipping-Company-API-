using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.CitySetting.Models
{
    public class CitySettingToUpdateDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal StandardShippingCost { get; set; }
        public decimal pickupShippingCost { get; set; }
        public DateTime CreatedAt { get; set; }
        //----------- Region ---------------------------------
        public int? RegionId { get; set; }
    }
}
