using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Repositories.contract;
// This Is Employee Repository Interface That Inharits From The Generic Repository Interface
public interface IEmployeeRepository:IGenericRepository<ApplicationUser,string>
{
    // This Method Is Used To Get All Employees With pramter (Pagination)
    Task<IEnumerable<ApplicationUser>> GetAllEmployeesAsync(Pramter pramter); 
}
