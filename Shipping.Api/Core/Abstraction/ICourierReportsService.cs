using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction;

public interface ICourierReportsService
{
    Task<List<GetAllCourierOrderCountDto>> GetAllCourierReportsAsync();
    Task<CourierReportDto> GetCourierReportByIdAsync(int id);
}
