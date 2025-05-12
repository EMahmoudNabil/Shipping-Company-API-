// APIs/Controllers/ShipmentController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly IShipmentService _shipmentService;
    private readonly IMapper _mapper;

    public ShipmentController(IShipmentService shipmentService, IMapper mapper)
    {
        _shipmentService = shipmentService;
        _mapper = mapper;
    }

    [HttpGet("track/{trackingNumber}")]
    public async Task<IActionResult> TrackShipment(string trackingNumber)
    {
        var shipment = await _shipmentService.GetShipmentByTrackingNumber(trackingNumber);
        if (shipment == null)
        {
            return NotFound(new { message = "Shipment not found" });
        }

        var shipmentDto = _mapper.Map<ShipmentTrackingDTO>(shipment);
        return Ok(shipmentDto);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserShipments(string userId)
    {
        var shipments = await _shipmentService.GetUserShipments(userId);
        var shipmentsDto = _mapper.Map<IEnumerable<ShipmentDTO>>(shipments);
        return Ok(shipmentsDto);
    }
}