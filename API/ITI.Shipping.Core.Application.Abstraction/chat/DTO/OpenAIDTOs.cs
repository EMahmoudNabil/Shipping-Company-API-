using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat.DTO
{
    public class OpenAIRequestDTO
    {
        public string Prompt { get; set; }
        public int MaxTokens { get; set; } = 150;
        public double Temperature { get; set; } = 0.7;
    }

    public class OpenAIResponseDTO
    {
        public string Response { get; set; }
    }
}
