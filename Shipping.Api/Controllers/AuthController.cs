// Ignore Spelling: Auth
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Infrastructure.Dtos;
namespace Shipping.Api.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO addEmployeeRequest,CancellationToken cancellationToken)
        {
            var response = await _userService.AddEmployeeAsync(addEmployeeRequest,cancellationToken);
            if(!string.IsNullOrEmpty(response))
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,response));
            return Created();
        }
        [HttpPost("addMerchant")]
        public async Task<IActionResult> AddMerchant(AddMerchantDTO addMerchantRequest,CancellationToken cancellationToken)
        {
            var response = await _userService.AddMerchantAsync(addMerchantRequest,cancellationToken);
            if(!string.IsNullOrEmpty(response))
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,response));
            return Created();
        }
        [HttpPost("addCourier")]
        public async Task<IActionResult> AddCourier(AddCourierDTO addCourierRequest,CancellationToken cancellationToken)
        {
            var response = await _userService.AddCourierAsync(addCourierRequest,cancellationToken);
            if(!string.IsNullOrEmpty(response))
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,response));
            return Created();
        }
    }
}
