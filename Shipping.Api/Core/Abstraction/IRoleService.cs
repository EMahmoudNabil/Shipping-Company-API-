using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDTO>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<RoleDetailsResponseDTO?> GetRoleByIdAsync(string roleId,CancellationToken cancellationToken = default);
    Task<string> CreateRoleAsync(CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken = default);
    Task<string> UpdateRoleAsync(string roleId,CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken = default);
    Task<string> DeleteRoleAsync(string roleId,CancellationToken cancellationToken = default);
}
