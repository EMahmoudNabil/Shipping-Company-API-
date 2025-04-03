using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction;

public interface IUserService
{
    Task<LoginResponseDTO?> GetTokenAsync(LoginDTO loginDTO,CancellationToken cancellationToken = default);
    Task<AccountProfileDTO?> GetAccountProfileAsync(string userId,CancellationToken cancellationToken = default);
    Task<string> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO,CancellationToken cancellationToken = default);
    Task<string> AddMerchantAsync(AddMerchantDTO addMerchantDTO,CancellationToken cancellationToken = default);
    Task<string> AddCourierAsync(AddCourierDTO addCourierDTO,CancellationToken cancellationToken = default);
}
