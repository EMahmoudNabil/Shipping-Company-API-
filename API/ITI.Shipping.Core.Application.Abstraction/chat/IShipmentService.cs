// Services/Interfaces/IShipmentService.cs
public interface IShipmentService
{
    Task<Shipment> GetShipmentByTrackingNumber(string trackingNumber);
    Task<IEnumerable<Shipment>> GetUserShipments(string userId);
    Task<ShipmentStatus> GetShipmentStatus(string trackingNumber);
    Task<string> GetCurrentLocation(string trackingNumber);
    Task<DateTime> GetEstimatedDeliveryDate(string trackingNumber);
}