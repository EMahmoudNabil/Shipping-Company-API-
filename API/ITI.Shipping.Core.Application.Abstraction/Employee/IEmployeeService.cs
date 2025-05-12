using ITI.Shipping.Core.Application.Abstraction.Employee.Model;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Employee;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(Pramter pramter);
    //Task<EmployeeDTO> GetEmployeeAsync(int id);
    //Task AddAsync(EmployeeToAddDTO DTO);
    //Task UpdateAsync(EmployeeToUpdateDTO DTO);
    //Task DeleteAsync(int id);
}
