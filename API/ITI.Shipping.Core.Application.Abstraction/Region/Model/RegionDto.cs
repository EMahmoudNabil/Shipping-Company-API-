using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Region.Model
{
    public class RegionDto
    {
        public int Id { get; set; }
        public required string Governorate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> CityName { get; set; } = new List<string>();
    }
}
