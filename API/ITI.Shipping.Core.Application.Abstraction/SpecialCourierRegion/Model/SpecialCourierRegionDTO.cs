using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model
{
    public class SpecialCourierRegionDTO
    {
        public int Id { get; set; }
        public string? RegionName { get; set; }
        public int RegionId { get; set; }
        public string? CourierId { get; set; } 
        public string? CourierName { get; set; }
    }
}
