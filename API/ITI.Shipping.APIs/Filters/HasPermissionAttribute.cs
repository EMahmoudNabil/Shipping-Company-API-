using Microsoft.AspNetCore.Authorization;

namespace ITI.Shipping.APIs.Filters;
public class HasPermissionAttribute(string permission):AuthorizeAttribute(permission)
{
}
