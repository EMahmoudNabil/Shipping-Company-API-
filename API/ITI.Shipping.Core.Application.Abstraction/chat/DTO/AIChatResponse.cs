using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat.DTO
{
    public class AIChatResponse
    {
        public List<QuickReplyDTO> QuickReplies;

        public string Response { get; set; }
        public MessageType ResponseType { get; set; }
        public Shipment ShipmentDetails { get; set; } // في حالة طلب تفاصيل شحنة
    }
}
