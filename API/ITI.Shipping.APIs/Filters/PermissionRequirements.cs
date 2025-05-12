using Microsoft.AspNetCore.Authorization;

namespace ITI.Shipping.APIs.Filters;
public class PermissionRequirements(string permission):IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}