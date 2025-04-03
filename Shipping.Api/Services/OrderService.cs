using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services;


    public class OrderServices :IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;


    public OrderServices(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager,IOrderRepository orderRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderWithProductsDto>> GetAllOrdersAsyncAndDeleteOrder()
        {
            var orders = await _unitOfWork.GetRepository<Order,int>().GetAllAsync();
            var ordersDto = _mapper.Map<List<OrderWithProductsDto>>(orders);
            foreach(var order in ordersDto)
            {
                var MerchantName = await _userManager.FindByIdAsync(order.MerchantName);
                order.MerchantName = MerchantName?.FullName;
            }

            return ordersDto;
        }
        public async Task<OrderWithProductsDto?> GetOrderByIdAsync(int id)
        {
            var findOrder = await _unitOfWork.GetRepository<Order,int>()
                .GetByIdAsync(id);

            if(findOrder == null || findOrder.IsDeleted)
            {
                return null;
        }

        var orderDto = _mapper.Map<OrderWithProductsDto>(findOrder);
        var MerchantName = await _userManager.FindByIdAsync(orderDto.MerchantName);
        orderDto.MerchantName = MerchantName?.FullName;
        return orderDto;
        }

        public async Task<bool> CreateOrderAsync(addOrderDto addorderDto)
        {
            var order = _mapper.Map<Order>(addorderDto);
            await _unitOfWork.GetRepository<Order,int>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAsync(updateOrderDto UpdateOrderDto)
        {
            var existingOrder = await _unitOfWork.GetRepository<Order,int>().GetByIdAsync(UpdateOrderDto.Id);
            if(existingOrder is null)
                return false;

            _mapper.Map(UpdateOrderDto,existingOrder);
            await _unitOfWork.GetRepository<Order,int>().UpdateAsync(existingOrder);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Order,int>();

            var order = await repository.GetByIdAsync(id);
            if(order is null)
                return false;

            order.IsDeleted = true;

            await repository.UpdateAsync(order);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> RestoreOrderAsync(int id)
        {
            var order = await _unitOfWork.GetRepository<Order,int>().GetByIdAsync(id);
            if(order is null || !order.IsDeleted)
                return false;

            order.IsDeleted = false;
            await _unitOfWork.GetRepository<Order,int>().UpdateAsync(order);
            await _unitOfWork.CompleteAsync();

            return true;
        }

    public async Task<List<OrderWithProductsDto>> GetOrdersByStatus(OrderStatus status)
    {
        var orders = await _orderRepository.GetOrdersByStatus(status);

        var OrdersDto = _mapper.Map<List<OrderWithProductsDto>>(orders);
        foreach(var order in OrdersDto)
        {
            var MerchantName = await _userManager.FindByIdAsync(order.MerchantName);
            order.MerchantName = MerchantName?.FullName;
        }
        return OrdersDto;

    }
}

