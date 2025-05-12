using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RegionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet] // Get : /api/Region
        [HasPermission(Permissions.ViewRegions)]
        public async Task <ActionResult<IEnumerable<RegionDto>>> GetAllRegion([FromQuery] Pramter pramter)
        {
            var regions = await _serviceManager.RegionService.GetRegionsAsync(pramter);
            return Ok(regions);
        }
        [HttpGet("{id}")] // Get : /api/Region/id
        [HasPermission(Permissions.ViewRegions)]
        public async Task <ActionResult<RegionDto>> GetRegion(int id)
        {
            var region = await _serviceManager.RegionService.GetRegionAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
        [HttpPost] // Post : /api/Region
        [HasPermission(Permissions.AddRegions)]
        public async Task<ActionResult<RegionDto>> AddRegion(RegionDto DTO)
        {
            if(DTO == null)
                return BadRequest("Invalid Region data");
            await _serviceManager.RegionService.AddAsync(DTO);
            return Ok();
        }
        [HttpPut("{id}")] // Put : /api/Region/id
        [HasPermission(Permissions.UpdateRegions)]
        public async Task<ActionResult<RegionDto>> UpdateRegion(int id,[FromBody] RegionDto DTO)
        {
            if(DTO == null || id != DTO.Id)
                return BadRequest("Invalid Region data");
            try
            {
                await _serviceManager.RegionService.UpdateAsync(DTO);
                return NoContent(); // 204 No Content (successful update)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")] // Delete : /api/Region/id
        [HasPermission(Permissions.DeleteRegions)]
        public async Task<ActionResult> DeletRegion(int id)
        {
            try
            {
                await _serviceManager.RegionService.DeleteAsync(id);
                return NoContent(); // 204 No Content (successful deletion)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
