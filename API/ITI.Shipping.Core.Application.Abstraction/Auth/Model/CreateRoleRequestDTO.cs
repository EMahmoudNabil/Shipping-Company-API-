using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Auth.Model;
public record CreateRoleRequestDTO(
        string RoleName,
        IEnumerable<string> Permissions
    );