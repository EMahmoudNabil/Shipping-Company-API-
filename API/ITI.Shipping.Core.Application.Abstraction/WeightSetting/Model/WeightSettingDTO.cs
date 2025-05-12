using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
public class WeightSettingDTO
{
    public int Id { get; set; }
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public decimal CostPerKg { get; set; }
    public DateTime CreatedAt { get; set; } 
}
