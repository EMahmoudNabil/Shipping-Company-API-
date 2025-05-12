using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion;
using ITI.Shipping.Core.Application.Abstraction.User.Model;
using ITI.Shipping.Core.Application.Abstraction.User;
using ITI.Shipping.Core.Domin.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Core.Application.Abstraction.Auth.Model;
using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Infrastructure.Presistence.Data;
using System.Diagnostics.Metrics;

namespace ITI.Shipping.Core.Application.Services.UserServices;
public class UsersService(
    UserManager<ApplicationUser> userManager,
    IJWTProvider jWTProvider,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ApplicationContext context):IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJWTProvider _jWTProvider = jWTProvider;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _context = context;


    // Add Courier
    public async Task<string> AddCourierAsync(AddCourierDTO addCourierDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == addCourierDTO.Email))
            return "Another user with the same Email is already exist";
        var user = _mapper.Map<ApplicationUser>(addCourierDTO);
        var result = await _userManager.CreateAsync(user,addCourierDTO.Password);
        if(!result.Succeeded)
            return string.Join(",",result.Errors.Select(e => e.Description));
       await _userManager.AddToRoleAsync(user,addCourierDTO.RoleName); // Courier Role
        foreach(var item in addCourierDTO.SpecialCourierRegions)
        {
            item.CourierId = user.Id;
        }
        var specialCourierRegion = _mapper.Map<List<SpecialCourierRegion>>(addCourierDTO.SpecialCourierRegions);
        await _unitOfWork.GetSpecialCourierRegionRepository().AddRangeAsync(specialCourierRegion);
        return string.Empty;
    }
    // Add Employee
    public async Task<string> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == addEmployeeDTO.Email))
            return "Another user with the same Email is already exist";
        var user = _mapper.Map<ApplicationUser>(addEmployeeDTO);
        var result = await _userManager.CreateAsync(user,addEmployeeDTO.Password);
        if(!result.Succeeded)
            return string.Join(",",result.Errors.Select(e => e.Description));
        await _userManager.AddToRoleAsync(user,addEmployeeDTO.RoleName);
        return string.Empty;
    }
    // Add Merchant
    public async Task<string> AddMerchantAsync(AddMerchantDTO addMerchantDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == addMerchantDTO.Email))
            return "Another user with the same Email is already exist";
        var user = _mapper.Map<ApplicationUser>(addMerchantDTO);
        var result = await _userManager.CreateAsync(user,addMerchantDTO.Password);
        if(!result.Succeeded)
            return string.Join(",",result.Errors.Select(e => e.Description));
       await _userManager.AddToRoleAsync(user,addMerchantDTO.RoleName); // Merchant Role
        if(addMerchantDTO.SpecialCityCosts is not null)
        {
            foreach(var specialCityCosts in addMerchantDTO.SpecialCityCosts)
            {
                specialCityCosts.MerchantId = user.Id;
            }
            var specialCityCost = _mapper.Map<List<SpecialCityCost>>(addMerchantDTO.SpecialCityCosts);
            await _unitOfWork.GetSpecialCityCostRepository().AddRangeAsync(specialCityCost);
        }
        return string.Empty;
    }
    // Get Account Profile
    public async Task<AccountProfileDTO?> GetAccountProfileAsync(string userId,CancellationToken cancellationToken = default)
    {
        var accountDetails = await _userManager.Users.FirstAsync(u => u.Id == userId);
        return _mapper.Map<AccountProfileDTO>(accountDetails);
    }
    // JTW Token
    public async Task<LoginResponseDTO?> GetTokenAsync(LoginDTO loginDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.FindByEmailAsync(loginDTO.Email) is not { } user)
            return null;
        if(!await _userManager.CheckPasswordAsync(user,loginDTO.Password))
            return null;
        var userRoles = await _userManager.GetRolesAsync(user);
        var userPermissions = await _context.Roles
            .Join(_context.RoleClaims,
                role => role.Id,
                roleClaim => roleClaim.RoleId,
                (role,roleClaim) => new
                {
                    role,
                    roleClaim
                })
                .Where(r => userRoles.Contains(r.role.Name))
                .Select(r => r.roleClaim.ClaimValue)
                .Distinct()
                .ToListAsync(cancellationToken);
        var (token, expiresIn) = _jWTProvider.GenerateJwtToken(user,userRoles,userPermissions);
        return new LoginResponseDTO
        (
            user.Id,
                user.Email!,
                user.FullName,
            token,
            expiresIn
        );
    }

    public async Task<ApplicationUser> GetByIdAsync(string userId)
    {
        try
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user ?? throw new ArgumentException("User not found");
        }
        catch (Exception ex)
        {
           
            throw;
        }
    }
}