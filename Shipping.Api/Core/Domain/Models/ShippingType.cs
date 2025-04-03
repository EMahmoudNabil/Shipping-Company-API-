

namespace Shipping.Api.Core.Domain.Models;

    public class ShippingType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal BaseCost { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //------------- ICollection From Order ------------------------------
        public virtual ICollection<Order> Branches { get; set; } = [];
    }

