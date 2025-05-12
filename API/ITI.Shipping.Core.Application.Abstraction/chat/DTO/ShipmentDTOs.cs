// APIs/DTOs/ShipmentDTOs.cs
public class ShipmentDTO
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

public class ShipmentTrackingDTO
{
    public string TrackingNumber { get; set; }
    public string CurrentLocation { get; set; }
    public ShipmentStatus Status { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
}