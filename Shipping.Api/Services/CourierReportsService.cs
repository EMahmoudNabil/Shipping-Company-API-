using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services;

public class CourierReportsService:ICourierReportsService
{
    private readonly IGenericRepository<CourierReport,int> _genericRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _map;
    public CourierReportsService(IGenericRepository<CourierReport,int> _genericRepository,IMapper _map,UserManager<ApplicationUser> userManager)
    {
        this._genericRepository = _genericRepository;
        this._map = _map;
        _userManager = userManager;
    }
    public async Task<List<GetAllCourierOrderCountDto>> GetAllCourierReportsAsync()
    {
        var courierReports = await _genericRepository.GetAllAsync();
        if(courierReports == null)
        {
            return null;
        }
        var reportsDto = _map.Map<List<CourierReportDto>>(courierReports);
        List<GetAllCourierOrderCountDto> getAllCourierOrderCountDto = new();
        getAllCourierOrderCountDto.Add(new GetAllCourierOrderCountDto
        {
            CourierName = reportsDto.Select(C => C.CourierName).FirstOrDefault(),
            OrdersCount = courierReports.Select(O => O.OrderId).Count()
        });

        return getAllCourierOrderCountDto;

    }
    public async Task<CourierReportDto> GetCourierReportByIdAsync(int id)
    {
        CourierReport CourierReport = await _genericRepository.GetByIdAsync(id);
        if(CourierReport == null)
        {
            return null!;
        }
        var reportsDto = _map.Map<CourierReportDto>(CourierReport);
        var applicationUser = await _userManager.FindByIdAsync(reportsDto.ClientName);
        reportsDto.ClientName = applicationUser?.StoreName ?? "Unknown StoreName";
        return reportsDto;
    }
}
