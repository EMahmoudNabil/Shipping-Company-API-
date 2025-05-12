using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.CourierReport;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.CourierReportServices
{
    internal class CourierReportService:ICourierReportService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        public CourierReportService(IUnitOfWork UnitOfWork,IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _Mapper = mapper;
        }
        // Get All Courier Reports
        public async Task<IEnumerable<GetAllCourierOrderCountDto>> GetAllCourierReportsAsync(Pramter pramter)
        {
            var courierReports = await _UnitOfWork.GetRepository<CourierReport,int>().GetAllAsync(pramter);
            if(courierReports == null)
            {
                throw new KeyNotFoundException($"CourierReport not found.");
            }
            var reportsDto = _Mapper.Map<List<CourierReportDto>>(courierReports);
            List<GetAllCourierOrderCountDto> getAllCourierOrderCountDto = new();
            getAllCourierOrderCountDto.Add(new GetAllCourierOrderCountDto
            {
                CourierName = reportsDto.Select(C => C.CourierName).FirstOrDefault(),
                OrdersCount = courierReports.Select(O => O.OrderId).Count()
            });
            return getAllCourierOrderCountDto;           
        }
        // Get Courier Report By Id
        public async Task<CourierReportDto> GetCourierReportByIdAsync(int id,Pramter pramter)
        {
            var CourierReport = await _UnitOfWork.GetRepository<CourierReport,int>().GetByIdAsync(id);
            if(CourierReport == null)
            {
                throw new KeyNotFoundException($"CitySetting with ID {id} not found.");
            }
            var reportsDto = _Mapper.Map<CourierReportDto>(CourierReport);
            var applicationUser = await _UnitOfWork.GetRepository<ApplicationUser,string>().GetAllAsync(pramter);
            var merchant = applicationUser.Where(u => u.Id == reportsDto.ClientName).FirstOrDefault();
            reportsDto.ClientName = merchant?.StoreName ?? "Unknown StoreName";
            return reportsDto;
        }
    }
}
