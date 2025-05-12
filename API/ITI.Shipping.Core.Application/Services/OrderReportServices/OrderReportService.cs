using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Application.Abstraction.OrderReport;
using ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.OrderReportServices
{
    internal class OrderReportService:IOrderReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderReportService(IUnitOfWork unitOfWork , IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        //---------------------------------------------------------------------------
        // Method to get Merchant Name and Amount Received and Shipping Cost Paid
        private async Task<IEnumerable<OrderReportToShowDTO>> GetMerchantNameAndAmountReceivedAndShippingCostPaid(IEnumerable<OrderReport> orderreports)
        {
            var orderreportsDto = _mapper.Map<IEnumerable<OrderReportToShowDTO>>(orderreports);
            foreach(var orderreport in orderreportsDto)
            {
                var MerchantName = await _userManager.FindByIdAsync(orderreport.MerchantId!);
                var Courier = await _userManager.FindByIdAsync(orderreport.CourierId!);
                orderreport.MerchantName = MerchantName?.FullName ?? "Unknown";
                orderreport.CompanyValue = Courier?.DeductionCompanyFromOrder ?? 0;
                switch(orderreport.PaymentType)
                {
                    case "Collectible":
                        orderreport.AmountReceived = orderreport.ShippingCost + orderreport.OrderCost;
                        orderreport.ShippingCostPaid = 0;
                        break;
                    case "Prepaid":
                        orderreport.AmountReceived = 00;
                        orderreport.ShippingCostPaid = orderreport.ShippingCost;
                        break;
                    case "Expulsion":
                        orderreport.AmountReceived = orderreport.ShippingCost;
                        orderreport.ShippingCostPaid = 0;
                        break;
                    default:
                        orderreport.AmountReceived = 0;
                        orderreport.ShippingCostPaid = 0;
                        break;
                }
            }
            return orderreportsDto;
        }
        //------------------------------------------------------------------------------
        // Get All Order Report
        //public async Task<IEnumerable<OrderReportToShowDTO>> GetAllOrderReportAsync(Pramter pramter)
        //{
        //    //return _mapper.Map<IEnumerable<OrderReportToShowDTO>>(await _unitOfWork.GetRepository<OrderReport,int>().GetAllAsync(pramter));
        //    var orderreports = await _unitOfWork.GetOrderReportRepository().GetAllAsync(pramter);
        //    var orderreportsDto = await GetMerchantNameAndAmountReceivedAndShippingCostPaid(orderreports);
        //    return orderreportsDto;
        //}

        // Get All Order Report By Pramter(Sort , Pagenation)
        public async Task<IEnumerable<OrderReportToShowDTO>> GetAllOrderReportAsync(OrderReportPramter pramter)
        {
            var orderreports = await _unitOfWork.GetOrderReportRepository().GetOrderReportByPramter(pramter);
            var orderreportsDto = await GetMerchantNameAndAmountReceivedAndShippingCostPaid(orderreports);
            return orderreportsDto;
        }
        // Get Order Report By Id
        public async Task<OrderReportDTO> GetOrderReportAsync(int id)
        {
            return _mapper.Map<OrderReportDTO>(await _unitOfWork.GetOrderReportRepository().GetByIdAsync(id));
        }
        // Add Order Report
        public async Task AddAsync(OrderReportDTO DTO)
        {
            await _unitOfWork.GetOrderReportRepository().AddAsync(_mapper.Map<OrderReport>(DTO));
            await _unitOfWork.CompleteAsync();
        }
        // Update Order Report
        public async Task UpdateAsync(OrderReportDTO DTO)
        {
            var OrderReportRepo = _unitOfWork.GetOrderReportRepository();

            var existingOrderReport = await OrderReportRepo.GetByIdAsync(DTO.Id);
            if(existingOrderReport == null)
                throw new KeyNotFoundException($"OrderReport with ID {DTO.Id} not found.");

            _mapper.Map(DTO,existingOrderReport); // Update existing entity with DTO data

            OrderReportRepo.UpdateAsync(existingOrderReport);
            await _unitOfWork.CompleteAsync();
        }
        // Delete Order Report
        public async Task DeleteAsync(int id)
        {
            var OrderReportRepo = _unitOfWork.GetOrderReportRepository();

            var existingOrderReport = await OrderReportRepo.GetByIdAsync(id);
            if(existingOrderReport == null)
                throw new KeyNotFoundException($"OrderReport with ID {id} not found.");
            await OrderReportRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }     
    }
}
