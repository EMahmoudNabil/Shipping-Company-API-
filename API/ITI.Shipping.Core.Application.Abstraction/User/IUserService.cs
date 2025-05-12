using ITI.Shipping.Core.Application.Abstraction.Auth.Model;
using ITI.Shipping.Core.Application.Abstraction.User.Model;
using ITI.Shipping.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.User;
public interface IUserService
{
    Task<LoginResponseDTO?> GetTokenAsync(LoginDTO loginDTO,CancellationToken cancellationToken = default);
    Task<AccountProfileDTO?> GetAccountProfileAsync(string userId,CancellationToken cancellationToken = default);
    Task<string> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO,CancellationToken cancellationToken = default);
    Task<string> AddMerchantAsync(AddMerchantDTO addMerchantDTO,CancellationToken cancellationToken = default);
    Task<string> AddCourierAsync(AddCourierDTO addCourierDTO,CancellationToken cancellationToken = default);
    Task<ApplicationUser> GetByIdAsync(string userId);
}
