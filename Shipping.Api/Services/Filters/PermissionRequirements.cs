using Microsoft.AspNetCore.Authorization;

namespace Shipping.Api.Services.Filters;

public class PermissionRequirements(string permission):IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
