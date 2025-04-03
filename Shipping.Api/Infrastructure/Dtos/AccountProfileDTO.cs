namespace Shipping.Api.Infrastructure.Dtos;

public record AccountProfileDTO(
        string Email,
        string FullName,
        string PhoneNumber,
        string Address
    );