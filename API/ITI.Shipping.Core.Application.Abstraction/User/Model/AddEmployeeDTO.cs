using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.User.Model;
public record AddEmployeeDTO(
        string Email,
        string Password,
        string FullName,
        string PhoneNumber,
        string Address,
        int BranchId,
        int RegionID,
        string RoleName
    );
