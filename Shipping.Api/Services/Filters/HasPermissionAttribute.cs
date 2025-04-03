using Microsoft.AspNetCore.Authorization;

namespace Shipping.Api.Services.Filters;

public class HasPermissionAttribute(string permission):AuthorizeAttribute(permission)
{
}
