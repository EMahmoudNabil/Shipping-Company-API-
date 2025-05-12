using ITI.Shipping.Core.Application.Abstraction.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Auth;
public interface IRoleService
{
    Task<IEnumerable<RoleResponseDTO>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<RoleDetailsResponseDTO?> GetRoleByIdAsync(string roleId,CancellationToken cancellationToken = default);
    Task<string> CreateRoleAsync(CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken = default);
    Task<string> UpdateRoleAsync(string roleId,CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken = default);
    Task<string> DeleteRoleAsync(string roleId,CancellationToken cancellationToken = default);
    Task<List<string>> GetUserPermissionsAsync(string userId);
}
