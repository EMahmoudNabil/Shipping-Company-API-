

using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Api.Core.Domain.Models;

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    //----------- Obj From Order and ForeignKey OrderId ---------------------------------

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order? Order { get; set; }
}

