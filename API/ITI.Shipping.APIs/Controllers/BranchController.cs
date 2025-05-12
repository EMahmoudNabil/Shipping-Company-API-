using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController:ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BranchController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet()] // Get : /api/Branch
        [HasPermission (Permissions.ViewBranches)]
        public async Task<ActionResult<IEnumerable<BranchDTO>>> GetBranches([FromQuery] Pramter pramter)
        {
            var branches = await _serviceManager.BranchService.GetBranchesAsync(pramter);
            return Ok(branches);
        }
        [HttpGet("{id}")] // Get : /api/Branch/id
        [HasPermission(Permissions.ViewBranches)]
        public async Task<ActionResult<BranchDTO>> GetBranch(int id)
        {
            var branch = await _serviceManager.BranchService.GetBranchAsync(id);
            if(branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }
        [HttpPost] // Post : /api/Branch
        [HasPermission(Permissions.AddBranches)]
        public async Task<ActionResult<BranchDTO>> AddBranch(BranchToAddDTO DTO)
        {
            if(DTO == null)
                return BadRequest("Invalid branch data");
            await _serviceManager.BranchService.AddAsync(DTO);
            return Ok();
        }
        [HttpPut("{id}")] // Put : /api/Branch/id
        [HasPermission(Permissions.UpdateBranches)]
        public async Task<ActionResult> UpdateBranch(int id,[FromBody] BranchToUpdateDTO DTO)
        {
            if(DTO == null || id != DTO.Id)
                return BadRequest("Invalid branch data.");
            try
            {
                await _serviceManager.BranchService.UpdateAsync(DTO);
                return NoContent(); // 204 No Content (successful update)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")] // Delete : /api/Branch/id
        [HasPermission(Permissions.DeleteBranches)]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            try
            {
                await _serviceManager.BranchService.DeleteAsync(id);
                return NoContent(); // 204 No Content (successful deletion)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
