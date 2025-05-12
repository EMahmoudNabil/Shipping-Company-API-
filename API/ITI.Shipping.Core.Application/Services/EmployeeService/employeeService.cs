using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Employee;
using ITI.Shipping.Core.Application.Abstraction.Employee.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Infrastructure.Presistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Services.EmployeeService;
public class employeeService:IEmployeeService
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public employeeService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }
    public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(Pramter pramter)
    {
        return  _Mapper.Map<IEnumerable<EmployeeDTO>>(await _UnitOfWork.GetAllEmployeesAsync().GetAllEmployeesAsync(pramter));
    }
}
