// Services/ShipmentService.cs
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public class ShipmentService : IShipmentService
{
    private readonly ApplicationContext _context;
    private readonly ILogger<ShipmentService> _logger;
    private readonly IMemoryCache _cache;
    public ShipmentService(ApplicationContext context, ILogger<ShipmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

   

    public async Task<Shipment> GetShipmentByTrackingNumber(string trackingNumber)
    {
        return await _cache.GetOrCreateAsync($"shipment_{trackingNumber}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return await _context.Shipments
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
        });
    }

    public async Task<IEnumerable<Shipment>> GetUserShipments(string userId)
    {
        return await _context.Shipments
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.ShippingDate)
            .ToListAsync();
    }

    public async Task<ShipmentStatus> GetShipmentStatus(string trackingNumber)
    {
        var shipment = await GetShipmentByTrackingNumber(trackingNumber);
        return shipment?.Status ?? ShipmentStatus.NotFound;
    }

    public async Task<string> GetCurrentLocation(string trackingNumber)
    {
        var shipment = await GetShipmentByTrackingNumber(trackingNumber);
        return shipment?.CurrentLocation ?? "Location not available";
    }

    public async Task<DateTime> GetEstimatedDeliveryDate(string trackingNumber)
    {
        var shipment = await GetShipmentByTrackingNumber(trackingNumber);
        return shipment?.EstimatedDeliveryDate ?? DateTime.MinValue;
    }
}