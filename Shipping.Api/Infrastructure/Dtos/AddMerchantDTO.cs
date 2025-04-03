using System.Text.Json.Serialization;

namespace Shipping.Api.Infrastructure.Dtos;

public record AddMerchantDTO(
        string Email,
        string Password,
        string FullName,
        string PhoneNumber,
        string Address,
        int BranchId,
        int RegionId,
        int CityId,
        string StoreName,
        List<SpecialCityCostDTO>? SpecialCityCosts
    );

public record SpecialCityCostDTO
{
    public decimal Price { get; set; }
    public int CitySettingId { get; set; }
    [JsonIgnore]
    public string MerchantId { get; set; } = string.Empty;
}