namespace Shipping.Api.Infrastructure.Dtos;

public record LoginDTO(
        string Email,
        string Password
    );