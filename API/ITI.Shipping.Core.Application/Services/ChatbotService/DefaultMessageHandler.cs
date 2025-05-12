// DefaultMessageHandler.cs
using ITI.Shipping.Core.Application.Abstraction.chat;
using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.ChatbotService
{
    public class DefaultMessageHandler : IMessageHandler
    {
        private readonly IOpenAIService _openAIService;

        public DefaultMessageHandler(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public bool CanHandle(string message)
        {
            return true;
        }

        public async Task<AIChatResponse> Handle(string message, string userId)
        {
            try
            {
                var systemMessage = "أنت مساعد ذكي لشركة شحن. يجب أن تكون الردود مختصرة ودقيقة. " +
                       "استخدم لغة المستخدم (عربي/إنجليزي) حسب لغته. " +
                       "إذا لم تكن متأكدًا من الإجابة، اطلب توضيحًا.";

                var prompt = $"System: {systemMessage}\nUser: {message}\nAssistant:";

                var response = await _openAIService.GetCompletionAsync(prompt);
                return new AIChatResponse
                {
                    Response = response.Choices.FirstOrDefault()?.Text.Trim() ?? GetDefaultResponse(message),
                    ResponseType = MessageType.Text,
                    QuickReplies = GenerateQuickReplies(message)
                };
            }
            catch
            {
                return new AIChatResponse
                {
                    Response = GetDefaultResponse(message),
                    ResponseType = MessageType.Text
                };
            }
        }

        private List<QuickReplyDTO> GenerateQuickReplies(string message)
        {
            var replies = new List<QuickReplyDTO>();

            if (message.Contains("مساعدة") || message.Contains("help"))
            {
                replies.Add(new QuickReplyDTO { Title = "تتبع شحنة", Payload = "كيف أتتبع شحنتي؟" });
                replies.Add(new QuickReplyDTO { Title = "مواعيد التوصيل", Payload = "ما هي مواعيد التوصيل؟" });
            }
            else
            {
                replies.Add(new QuickReplyDTO { Title = "تتبع شحنة", Payload = "أريد تتبع شحنتي" });
                replies.Add(new QuickReplyDTO { Title = "مساعدة", Payload = "أحتاج مساعدة" });
            }

            return replies;
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
    }
}