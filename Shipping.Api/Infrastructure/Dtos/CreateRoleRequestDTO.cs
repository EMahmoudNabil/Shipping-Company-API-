namespace Shipping.Api.Infrastructure.Dtos;

public record CreateRoleRequestDTO(
        string RoleName,
        IEnumerable<string> Permissions
    );