using System.ComponentModel.DataAnnotations;


namespace Shipping.Api.Core.Domain.Models;

    public class WeightSetting
    {
        public int Id { get; set; }
        [Required]
        public decimal MinWeight { get; set; }
        [Required]
        public decimal MaxWeight { get; set; }
        [Required]
        public decimal CostPerKg { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

