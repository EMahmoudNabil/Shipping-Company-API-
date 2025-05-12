using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using ITI.Shipping.Core.Application.Abstraction.chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ITI.Shipping.Core.Application.Services.ChatbotService
{
    public class AIService : IAIService
    {
        private readonly IShipmentService _shipmentService;
        private readonly IOpenAIService _openAIService;
        private readonly List<IMessageHandler> _messageHandlers;

        public AIService(
            IOpenAIService openAIService,
            IShipmentService shipmentService)
        {
            _openAIService = openAIService;
            _shipmentService = shipmentService;

            _messageHandlers = new List<IMessageHandler>
            {
                new TrackingMessageHandler(_shipmentService),
                new DefaultMessageHandler(_openAIService)
            };
        }

        public async Task<AIChatResponse> ProcessUserMessageAsync(string message, string userId)
        {
            foreach (var handler in _messageHandlers)
            {
                if (handler.CanHandle(message))
                {
                    return await handler.Handle(message, userId);
                }
            }

            return await _messageHandlers.Last().Handle(message, userId);
        }

        public async Task<Shipment> GetShipmentDetailsAsync(string trackingNumber)
        {
            return await _shipmentService.GetShipmentByTrackingNumber(trackingNumber);
        }

        #region Private Helper Methods

        private bool IsTrackingRequest(string message)
        {
            var trackingKeywords = new[] { "تتبع", "track", "شحنة", "شحنتي", "تتبع شحنتي" };
            return trackingKeywords.Any(keyword =>
                message.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<AIChatResponse> HandleTrackingRequest(string message)
        {
            var trackingNumber = ExtractTrackingNumber(message);
            if (!string.IsNullOrEmpty(trackingNumber))
            {
                var shipment = await GetShipmentDetailsAsync(trackingNumber);
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

        private List<QuickReplyDTO> GenerateQuickReplies(string message)
        {
            var replies = new List<QuickReplyDTO>();

            if (message.Contains("مساعدة") || message.Contains("help"))
            {
                replies.Add(new QuickReplyDTO { Title = "تتبع شحنة", Payload = "كيف أتتبع شحنتي؟" });
                replies.Add(new QuickReplyDTO { Title = "مواعيد التوصيل", Payload = "ما هي مواعيد التوصيل؟" });
                replies.Add(new QuickReplyDTO { Title = "اتصال بمسؤول", Payload = "أريد التحدث مع مسؤول" });
            }
            else
            {
                replies.Add(new QuickReplyDTO { Title = "تتبع شحنة", Payload = "أريد تتبع شحنتي" });
                replies.Add(new QuickReplyDTO { Title = "مساعدة", Payload = "أحتاج مساعدة" });
            }

            return replies;
        }

        // يمكن تحسين regex لالتقاط المزيد من أنماط أرقام التتبع
        private string ExtractTrackingNumber(string message)
        {
            var pattern = @"\b([A-Z]{2}\d{9}[A-Z]{2}|\d{12,15}|[A-Z0-9]{8,})\b";
            var match = Regex.Match(message, pattern);
            return match.Success ? match.Value : string.Empty; // Return an empty string instead of null
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

        private async Task<string> GenerateGenericResponse(string message)
        {
            try
            {

                var systemMessage = "أنت مساعد ذكي لشركة شحن. يجب أن تكون الردود مختصرة ودقيقة. " +
                       "استخدم لغة المستخدم (عربي/إنجليزي) حسب لغته. " +
                       "إذا لم تكن متأكدًا من الإجابة، اطلب توضيحًا.";

                var prompt = $"System: {systemMessage}\nUser: {message}\nAssistant:";
                // استخدام خدمة OpenAI للرد الذكي
                var response = await _openAIService.GetCompletionAsync(prompt);
                return response.Choices.FirstOrDefault()?.Text.Trim() ?? GetDefaultResponse(message);
            }
            catch
            {
                return GetDefaultResponse(message);
            }
        }

        private string GetDefaultResponse(string message)
        {
            var greetings = new[] { "مرحباً", "أهلاً", "تحية طيبة" };
            if (greetings.Any(g => message.Contains(g, StringComparison.OrdinalIgnoreCase)))
            {
                return "أهلاً بك! كيف يمكنني مساعدتك اليوم؟";
            }

            var thanks = new[] { "شكراً", "متشكر", "ممتن" };
            if (thanks.Any(t => message.Contains(t, StringComparison.OrdinalIgnoreCase)))
            {
                return "العفو! دائماً سعيد بمساعدتك. هل هناك شيء آخر تحتاج إليه؟";
            }

            return "أنا هنا لمساعدتك في استفساراتك عن الشحن والتوصيل. يمكنك سؤالي عن حالة شحنة أو أي شيء آخر يتعلق بخدمتنا.";
        }

        #endregion
    }
}