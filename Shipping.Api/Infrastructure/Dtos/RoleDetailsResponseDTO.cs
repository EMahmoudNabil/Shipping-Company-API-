namespace Shipping.Api.Infrastructure.Dtos;

public record RoleDetailsResponseDTO(
        string RoleId,
        string RoleName,
        string CreatedAt,
        IEnumerable<string> Permissions
    );
