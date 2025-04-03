using Shipping.Api.Core.Domain.Helpers;
using System.Text.Json.Serialization;

namespace Shipping.Api.Infrastructure.Dtos;

public record AddCourierDTO(
        string Email,
        string FullName,
        string PhoneNumber,
        string Address,
        string Password,
        int BranchId,
        DeductionTypes DeductionType,
        decimal DeductionCompanyFromOrder,
        List<SpecialCourierRegionDTO> SpecialCourierRegions
    );

public record SpecialCourierRegionDTO
{
    public int RegionId { get; set; }
    [JsonIgnore]
    public string CourierId { get; set; } = string.Empty;
}