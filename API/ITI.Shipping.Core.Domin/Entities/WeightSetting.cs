using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
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
}