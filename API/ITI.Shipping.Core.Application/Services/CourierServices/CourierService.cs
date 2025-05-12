using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Courier;
using ITI.Shipping.Core.Application.Abstraction.Courier.DTO;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.CourierServices;
internal class CourierService:ICourierService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public CourierService(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager)
    {
       _unitOfWork = unitOfWork;
       _mapper = mapper;
       _userManager = userManager;
    }
    // Get All Courier 
    public async Task<IEnumerable<CourierDTO>> GetAllAsync(Pramter pramter)
    {
        return _mapper.Map<IEnumerable<CourierDTO>> (await _unitOfWork.GetRepository<ApplicationUser,string>().GetAllAsync(pramter));
    }
    // Get Courier By Branch
    public async Task<IEnumerable<CourierDTO>> GetCourierByBranch(int OrderId)
    {
       var order = await _unitOfWork.GetOrderRepository().GetByIdAsync(OrderId);
        var Courieres = await _userManager.GetUsersInRoleAsync(DefaultRole.Courier);
        var couriersInBranch  = Courieres.Where(c => c.BranchId == order!.BranchId);
        var couriersDto = _mapper.Map<IEnumerable<CourierDTO>>(couriersInBranch);
        return couriersDto;
    }
    // Get Courier By Region
    public async Task<IEnumerable<CourierDTO>> GetCourierByRegion(int RegionId)
    {
        var region = await _unitOfWork.GetRepository<Region,int>().GetByIdAsync(RegionId);
        var Courieres = await _userManager.GetUsersInRoleAsync(DefaultRole.Courier);
        var couriersInBranch = Courieres.Where(c => c.RegionId == region!.Id);
        var couriersDto = _mapper.Map<IEnumerable<CourierDTO>>(couriersInBranch);
        return couriersDto;
    }
}
