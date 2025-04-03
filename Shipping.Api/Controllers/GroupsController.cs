using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IRoleService roleService):ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet("")]
        public async Task<IActionResult> GetAllGroups(CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllRolesAsync(cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(string id,CancellationToken cancellationToken)
        {
            var result = await _roleService.GetRoleByIdAsync(id,cancellationToken);
            if(result is null)
                return NotFound(new ResponseAPI(StatusCodes.Status400BadRequest,"Group does not exists"));
            return Ok(result);
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateGroup(CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken)
        {
            var result = await _roleService.CreateRoleAsync(createRoleRequestDTO,cancellationToken);
            if(result != "Group created successfully")
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,result));
            return Ok(new ResponseAPI(StatusCodes.Status201Created,result));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(string id,CreateRoleRequestDTO createRoleRequestDTO,CancellationToken cancellationToken)
        {
            var result = await _roleService.UpdateRoleAsync(id,createRoleRequestDTO,cancellationToken);
            if(result != "Group updated successfully")
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,result));
            return Ok(new ResponseAPI(StatusCodes.Status200OK,result));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(string id,CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(id,cancellationToken);
            if(result != "Group deleted successfully")
                return BadRequest(new ResponseAPI(StatusCodes.Status400BadRequest,result));
            return Ok(new ResponseAPI(StatusCodes.Status204NoContent,result));
        }
    }
}
