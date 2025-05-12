using ITI.Shipping.Core.Application.Abstraction.Auth.Model;
using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ITI.Shipping.Core.Application.Services.AuthServices;
public class RoleService(RoleManager<ApplicationRole> roleManager,ApplicationContext context , UserManager<ApplicationUser> userManager) :IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly ApplicationContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager;
    // Get all Roles (Group)
    public async Task<IEnumerable<RoleResponseDTO>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        return await _roleManager.Roles
            .Where(r => !r.IsDeleted)
            .Select(r => new RoleResponseDTO(
                    r.Id,
                    r.Name!,
                    r.CreatedAt.ToShortDateString()
            )).ToListAsync(cancellationToken);
    }
    // Get Role (Group) By Id
    public async Task<RoleDetailsResponseDTO?> GetRoleByIdAsync(string roleId,CancellationToken cancellationToken = default)
    {
        if(await _roleManager.FindByIdAsync(roleId) is not { } role)
            return null;
        var permissions = await _roleManager.GetClaimsAsync(role);
        return new RoleDetailsResponseDTO(
            role.Id,
            role.Name!,
            role.CreatedAt.ToShortDateString(),
            permissions.Select(p => p.Value)
        );
    }
    // Create Role (Group)
    public async Task<string> CreateRoleAsync(CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken = default)
    {
        var roleIsExists = await _roleManager.RoleExistsAsync(createRoleRequestDTO.RoleName);
        if(roleIsExists)
            return "Role already exists";
        var allowedPermissions = Permissions.GetAllPermissions();
        if(createRoleRequestDTO.Permissions.Except(allowedPermissions).Any())
            return "Invalid permissions";
        var role = new ApplicationRole
        {
            Name = createRoleRequestDTO.RoleName,
            ConcurrencyStamp = Guid.CreateVersion7().ToString()
        };
        var result = await _roleManager.CreateAsync(role);
        if(!result.Succeeded)
            return "Failed to create role";
        var permissions = createRoleRequestDTO.Permissions.Select(p => new IdentityRoleClaim<string>
        {
            ClaimType = Permissions.Type,
            ClaimValue = p,
            RoleId = role.Id
        });
        await _context.RoleClaims.AddRangeAsync(permissions,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return "Group Created Successfully";
    }
    // Update Role (Group)
    public async Task<string> UpdateRoleAsync(string roleId,CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken = default)
    {
        var roleIsExists = await _roleManager.Roles.AnyAsync(r => r.Name == createRoleRequestDTO.RoleName && r.Id != roleId);
        if(roleIsExists)
            return "Role already exists";
        if(await _roleManager.FindByIdAsync(roleId) is not { } role)
            return "Role does not exists";
        var allowedPermissions = Permissions.GetAllPermissions();
        if(createRoleRequestDTO.Permissions.Except(allowedPermissions).Any())
            return "Invalid permissions";
        role.Name = createRoleRequestDTO.RoleName;
        var result = await _roleManager.UpdateAsync(role);
        if(!result.Succeeded)
            return "Failed to update role";
        var permissions = await _context.RoleClaims
            .Where(c => c.RoleId == roleId && c.ClaimType == Permissions.Type)
            .Select(c => c.ClaimValue)
            .ToListAsync(cancellationToken);
        var permissionsToAdd = createRoleRequestDTO.Permissions.Except(permissions).Select(p => new IdentityRoleClaim<string>
        {
            ClaimType = Permissions.Type,
            ClaimValue = p,
            RoleId = role.Id
        });
        var permissionsToRemove = permissions.Except(createRoleRequestDTO.Permissions);
        await _context.RoleClaims
            .Where(rc => rc.RoleId == roleId && permissionsToRemove.Contains(rc.ClaimValue))
            .ExecuteDeleteAsync(cancellationToken);
        await _context.RoleClaims.AddRangeAsync(permissionsToAdd,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return "Group Updated Successfully";
    }
    // Delete Role (Group)
    public async Task<string> DeleteRoleAsync(string roleId,CancellationToken cancellationToken = default)
    {
        if(await _roleManager.FindByIdAsync(roleId) is not { } role)
            return "Role does not exists";
        role.IsDeleted = true;
        var result = await _roleManager.UpdateAsync(role);
        if(!result.Succeeded)
            return "Failed to delete role";
        return "Group Deleted Successfully";
    }
    public async Task<List<string>> GetUserPermissionsAsync(string userId)
    {
        // Get all role IDs for the user
        var roleIds = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

        if (!roleIds.Any())
            return new List<string>();

        // Get all permissions for these roles
        var permissions = await _context.RoleClaims
            .Where(rc => roleIds.Contains(rc.RoleId) && rc.ClaimType == Permissions.Type)
            .Select(rc => rc.ClaimValue!)
            .Distinct()
            .ToListAsync();

        return permissions;
    }

}
