using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
public class SpecialCityCostDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
    public string MerchantId { get; set; }=string.Empty;
    public string? MerchantName { get; set; }
    public int CitySettingId { get; set; }
    public string? CitySettingName { get; set;}
}
