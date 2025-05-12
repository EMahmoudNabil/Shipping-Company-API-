using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat
{
    // تعريف واجهة معالج الرسائل
    public interface IMessageHandler
    {
        bool CanHandle(string message);
        Task<AIChatResponse> Handle(string message, string userId);
    }

}
