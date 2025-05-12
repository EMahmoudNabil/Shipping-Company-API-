using ITI.Shipping.Core.Application.Abstraction.Auth.Model;
using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Core.Domin.ResponseHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Domin.Entities_Helper;
using System.Security.Claims;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GroupsController(IRoleService roleService):ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    [HasPermission(Permissions.ViewPermissions)]
    public async Task<IActionResult> GetAllGroups(CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(result);
    }
    [HttpGet("{id}")]
    [HasPermission(Permissions.ViewPermissions)]
    public async Task<IActionResult> GetGroup(string id,CancellationToken cancellationToken)
    {
        var result = await _roleService.GetRoleByIdAsync(id,cancellationToken);
        if(result is null)
            return NotFound(new ResponseAPI(StatusCodes.Status400BadRequest,"Group does not exists"));
        return Ok(result);
    }
    [HttpPost("")]
    [HasPermission(Permissions.AddPermissions)]
    public async Task<IActionResult> CreateGroup(CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken)
    {
        var result = await _roleService.CreateRoleAsync(createRoleRequestDTO,cancellationToken);
        if(result.Equals( "Group Created Successfully"))
            return Ok(new ResponseAPI(StatusCodes.Status201Created,result));
        return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,result));

    }
    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdatePermissions)]
    public async Task<IActionResult> UpdateGroup(string id,CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken)
    {
        var result = await _roleService.UpdateRoleAsync(id,createRoleRequestDTO,cancellationToken);
        if(result.Equals("Group Updated Successfully"))
            return Ok(new ResponseAPI(StatusCodes.Status200OK,result));
        return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,result));

    }
    [HttpDelete("{id}")]
    [HasPermission(Permissions.DeletePermissions)]
    public async Task<IActionResult> DeleteGroup(string id,CancellationToken cancellationToken)
    {
        var result = await _roleService.DeleteRoleAsync(id,cancellationToken);
        if(result.Equals("Group Deleted Successfully"))
         return Ok(new ResponseAPI(StatusCodes.Status204NoContent,result));
        return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,result));

    }
    [HttpGet("permissions/me")]
    public async Task<IActionResult> GetCurrentUserPermissions()
    {
        // احصل على ID المستخدم الحالي من التوكن
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var permissions = await _roleService.GetUserPermissionsAsync(userId);
        return Ok(permissions);
    }
}
