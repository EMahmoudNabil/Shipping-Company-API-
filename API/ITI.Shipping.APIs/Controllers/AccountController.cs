using ITI.Shipping.Core.Application.Abstraction.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
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