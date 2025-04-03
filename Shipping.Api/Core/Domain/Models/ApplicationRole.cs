

using Microsoft.AspNetCore.Identity;

namespace Shipping.Api.Core.Domain.Models;
public class ApplicationRole:IdentityRole
{
    public ApplicationRole()
    {
        Id = Guid.CreateVersion7().ToString();
    }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}

