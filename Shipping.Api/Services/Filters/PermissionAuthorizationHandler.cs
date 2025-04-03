using Microsoft.AspNetCore.Authorization;

namespace Shipping.Api.Services.Filters;

public class PermissionAuthorizationHandler:AuthorizationHandler<PermissionRequirements>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,PermissionRequirements requirement)
    {
        if(context.User.Identity is not { IsAuthenticated: true } ||
            !context.User.Claims.Any(c => c.Type == "permissions" && c.Value == requirement.Permission))
            return;
        context.Succeed(requirement);
        return;
    }
}
