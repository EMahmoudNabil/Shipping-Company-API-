// TrackingMessageHandler.cs
using ITI.Shipping.Core.Application.Abstraction.chat;
using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.ChatbotService
{
    public class TrackingMessageHandler : IMessageHandler
    {
        private readonly IShipmentService _shipmentService;

        public TrackingMessageHandler(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public bool CanHandle(string message)
        {
            var trackingKeywords = new[] { "تتبع", "track", "شحنة", "شحنتي", "تتبع شحنتي" };
            return trackingKeywords.Any(keyword => message.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<AIChatResponse> Handle(string message, string userId)
        {
            var trackingNumber = ExtractTrackingNumber(message);
            if (!string.IsNullOrEmpty(trackingNumber))
            {
                var shipment = await _shipmentService.GetShipmentByTrackingNumber(trackingNumber);
                if (shipment != null)
                {
                    return new AIChatResponse
                    {
                        Response = GenerateShipmentStatusMessage(shipment),
                        ResponseType = MessageType.TrackingUpdate,
                        ShipmentDetails = shipment
                    };
                }
                return new AIChatResponse
                {
                    Response = "عفواً، لم أتمكن من العثور على الشحنة بهذا الرقم. يرجى التأكد من رقم التتبع والمحاولة مرة أخرى.",
                    ResponseType = MessageType.Text
                };
            }
            return new AIChatResponse
            {
                Response = "الرجاء إدخال رقم التتبع الخاص بالشحنة.",
                ResponseType = MessageType.Text
            };
        }

        private string ExtractTrackingNumber(string message)
        {
            var pattern = @"\b([A-Z]{2}\d{9}[A-Z]{2}|\d{12,15}|[A-Z0-9]{8,})\b";
            var match = Regex.Match(message, pattern);
            return match.Success ? match.Value : string.Empty;
        }

        private string GenerateShipmentStatusMessage(Shipment shipment)
        {
            return $"حالة الشحنة رقم {shipment.TrackingNumber}:\n" +
                   $"الموقع الحالي: {shipment.CurrentLocation}\n" +
                   $"الحالة: {GetArabicStatus(shipment.Status)}\n" +
                   $"التاريخ المتوقع للتسليم: {shipment.EstimatedDeliveryDate.ToShortDateString()}";
        }

        private string GetArabicStatus(ShipmentStatus status)
        {
            return status switch
            {
                ShipmentStatus.Pending => "قيد الانتظار",
                ShipmentStatus.InTransit => "في الطريق",
                ShipmentStatus.OutForDelivery => "خارج للتوصيل",
                ShipmentStatus.Delivered => "تم التسليم",
                ShipmentStatus.Delayed => "متأخر",
                ShipmentStatus.Returned => "تم الإرجاع",
                ShipmentStatus.NotFound => "غير موجود",
                _ => "حالة غير معروفة"
            };
        }
    }
}