// Models/Shipment.cs
public class Shipment
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; }
    public string UserId { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public string CurrentLocation { get; set; }
    public ShipmentStatus Status { get; set; }
    public string DestinationAddress { get; set; }
    public string OriginAddress { get; set; }
}

public enum ShipmentStatus
{
    Pending,
    InTransit,
    OutForDelivery,
    Delivered,
    Delayed,
    Returned,
    NotFound
}