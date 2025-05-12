using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialCourierRegionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SpecialCourierRegionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet] // Get : /api/SpecialCourierRegion
        [HasPermission(Permissions.ViewRegions)]
        public async Task<ActionResult<IEnumerable<SpecialCourierRegionDTO>>> GetAllSpecialCourierRegions([FromQuery] Pramter pramter)
        {
            var SpecialCourierRegions = await _serviceManager.SpecialCourierRegionService.GetSpecialCourierRegionsAsync(pramter);
            return Ok(SpecialCourierRegions);
        }
        [HttpGet("{id}")] // Get : /api/SpecialCourierRegion/id
        [HasPermission(Permissions.ViewRegions)]
        public async Task<ActionResult<SpecialCourierRegionDTO>> GetSpecialCourierRegion(int id)
        {
            var SpecialCourierRegion = await _serviceManager.SpecialCourierRegionService.GetSpecialCourierRegionAsync(id);
            return Ok(SpecialCourierRegion);
        }
        [HttpPost] // Post : /api/SpecialCourierRegion
        [HasPermission(Permissions.AddRegions)]
        public async Task<ActionResult<SpecialCourierRegionDTO>> AddSpecialCourierRegion(SpecialCourierRegionDTO DTO)
        {
            if(DTO == null)
                return BadRequest("Invalid SpecialCourierRegion data");
            await _serviceManager.SpecialCourierRegionService.AddAsync(DTO);
            return Ok();
        }
        [HttpPut("{id}")] // Put : /api/SpecialCourierRegion/id
        [HasPermission(Permissions.UpdateRegions)]
        public async Task<ActionResult> UpdateSpecialCourierRegion(int id ,[FromBody] SpecialCourierRegionDTO DTO)
        {
            if(DTO == null || id != DTO.Id)
                return BadRequest("Invalid SpecialCourierRegion data.");
            try
            {
                await _serviceManager.SpecialCourierRegionService.UpdateAsync(DTO);
                return NoContent(); // 204 No Content (successful update)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")] // Delete : /api/SpecialCourierRegion/id
        [HasPermission(Permissions.DeleteRegions)]
        public async Task<ActionResult> DeleteSpecialCourierRegion(int id)
        {
            try
            {
                await _serviceManager.SpecialCourierRegionService.DeleteAsync(id);
                return NoContent(); // 204 No Content (successful deletion)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
       
    }
}
