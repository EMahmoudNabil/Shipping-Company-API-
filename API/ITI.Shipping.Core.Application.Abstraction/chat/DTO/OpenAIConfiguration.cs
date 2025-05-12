using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat.DTO
{
    // Core/Application/Configuration/OpenAIConfiguration.cs
    public class OpenAIConfiguration
    {
        public string ApiKey { get; set; } 
        public string Model { get; set; } = "text-davinci-003";
        public int MaxTokens { get; set; } = 1000;
        public double Temperature { get; set; } = 0.7;
    }
}
