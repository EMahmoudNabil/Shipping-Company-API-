using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class CitySetting
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal StandardShippingCost { get; set; }
        public decimal pickupShippingCost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //------------- ICollection From User ------------------------------
        public virtual ICollection<ApplicationUser> Users { get; set; } = [];
        //----------- Obj From Region and ForeignKey RegionId ---------------------------------
        [ForeignKey(nameof(Region))]
        public int? RegionId { get; set; }
        public virtual Region? Region { get; set; }
        //------------- ICollection From Order ------------------------------
        public virtual ICollection<Order> Orders { get; set; } = [];
        //------------- ICollection From SpecialPickup ------------------------------
        public virtual ICollection<SpecialCityCost> SpecialPickups { get; set; } = [];
    }
}
