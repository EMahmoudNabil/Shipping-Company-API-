namespace Shipping.Api.Infrastructure.Dtos;

public record AddEmployeeDTO(
        string Email,
        string Password,
        string FullName,
        string PhoneNumber,
        string Address,
        int BranchId,
        int RegionID,
        string RoleName
    );
