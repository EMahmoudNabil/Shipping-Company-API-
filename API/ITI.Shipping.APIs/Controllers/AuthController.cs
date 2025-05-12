using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction.Auth.Model;
using ITI.Shipping.Core.Application.Abstraction.User;
using ITI.Shipping.Core.Application.Abstraction.User.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.ResponseHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserService userService):ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginRequest,CancellationToken cancellationToken)
    {
        var response = await _userService.GetTokenAsync(loginRequest,cancellationToken);
        if(response is null)
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,"Invalid Email or Password!"));
        return Ok(response);
    }
    [HttpPost("addEmployee")]
    [HasPermission(Permissions.AddEmployees)]
    public async Task<IActionResult> AddEmployee(AddEmployeeDTO addEmployeeRequest,CancellationToken cancellationToken)
    {
        var response = await _userService.AddEmployeeAsync(addEmployeeRequest,cancellationToken);
        if(!string.IsNullOrEmpty(response))
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,response));
        return Created();
    }
    [HttpPost("addMerchant")]
    [HasPermission(Permissions.AddMerchants)]
    public async Task<IActionResult> AddMerchant(AddMerchantDTO addMerchantRequest,CancellationToken cancellationToken)
    {
        var response = await _userService.AddMerchantAsync(addMerchantRequest,cancellationToken);
        if(!string.IsNullOrEmpty(response))
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,response));
        return Created();
    }
    [HttpPost("AddCourier")]
    [HasPermission(Permissions.AddCouriers)]
    public async Task<IActionResult> AddCourier([FromBody] AddCourierDTO addCourierRequest,CancellationToken cancellationToken)
    {
        var response = await _userService.AddCourierAsync(addCourierRequest,cancellationToken);
        if(!string.IsNullOrEmpty(response))
            return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,response));
        return Created();
    }
}
