using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
// This Is Employee Repository Class That Implemnts The IEmployeeRepository Interface
public class EmployeeRepository:GenericRepository<ApplicationUser,string>, IEmployeeRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public EmployeeRepository(ApplicationContext _Context,UserManager<ApplicationUser> userManager) : base(_Context)
    {
        _userManager = userManager;
    }
    // This Method Is Used To Get All Employees With pramter (Pagination)
    public async Task<IEnumerable<ApplicationUser>> GetAllEmployeesAsync(Pramter pramter)
    {
        var merchantIds = (await _userManager.GetUsersInRoleAsync(DefaultRole.Merchant)).Select(u => u.Id);
        var courierIds = (await _userManager.GetUsersInRoleAsync(DefaultRole.Courier)).Select(u => u.Id);
        var adminIds = (await _userManager.GetUsersInRoleAsync(DefaultRole.Admin)).Select(u => u.Id);
        var excludedIds = merchantIds
       .Concat(courierIds)
       .Concat(adminIds)
       .ToHashSet();
        var allEmployees = _context.Users
       .Where(u => !excludedIds.Contains(u.Id));
        
        if(pramter.PageSize != null && pramter.PageNumber != null)
        {
            return allEmployees
                .Skip((pramter.PageNumber.Value - 1) * pramter.PageSize.Value)
                .Take(pramter.PageSize.Value);
        }
        else
        {
            return allEmployees;
        }
    }

}
