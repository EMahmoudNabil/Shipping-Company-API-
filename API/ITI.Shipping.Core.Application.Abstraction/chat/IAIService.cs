using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat
{
    public interface IAIService
    {
        Task<AIChatResponse> ProcessUserMessageAsync(string message, string userId);
        Task<Shipment> GetShipmentDetailsAsync(string trackingNumber);
    }
}
