namespace Shipping.Api.Infrastructure.Dtos;

public record LoginResponseDTO(
        string ID,
        string Email,
        string FullName,
        string Token,
        int ExpiresIn
    );
