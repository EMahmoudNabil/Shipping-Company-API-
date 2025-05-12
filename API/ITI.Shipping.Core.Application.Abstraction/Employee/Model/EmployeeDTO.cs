using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Employee.Model;
public record EmployeeDTO
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
   // public string Role { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
