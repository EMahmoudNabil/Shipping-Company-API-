using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat.DTO
{
    // APIs/DTOs/ChatbotDTOs.cs
    public class ChatResponseDTO
    {
        public ChatMessageDTO UserMessage { get; set; }
        public ChatMessageDTO BotResponse { get; set; }
        public ShipmentDTO ShipmentDetails { get; set; } // في حالة رد يتعلق بشحنة
    }
}
