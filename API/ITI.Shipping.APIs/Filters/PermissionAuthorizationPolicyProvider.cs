using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ITI.Shipping.APIs.Filters;
public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options):DefaultAuthorizationPolicyProvider(options)
{
    private readonly AuthorizationOptions _options = options.Value;
    public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if(policy is not null)
            return policy;
        var permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirements(policyName))
            .Build();
        _options.AddPolicy(policyName,permissionPolicy);
        return permissionPolicy;
    }
}