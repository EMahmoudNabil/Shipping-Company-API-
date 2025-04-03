using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using System.Security.Claims;
namespace Shipping.Api.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    public class AccountController(IUserService userService):ControllerBase
    {
        private readonly IUserService _userService = userService;
        [HttpGet("")]
        public async Task<IActionResult> GetAccountProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountProfile = await _userService.GetAccountProfileAsync(userId!);
            return Ok(accountProfile);
        }
    }
}
