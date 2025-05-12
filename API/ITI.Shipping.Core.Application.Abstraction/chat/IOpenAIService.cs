using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.chat
{
    public interface IOpenAIService
    {
        Task<OpenAICompletionResponse> GetCompletionAsync(string prompt);
    }

    public class OpenAICompletionResponse
    {
        public List<OpenAIChoice> Choices { get; set; }
    }

    public class OpenAIChoice
    {
        public string Text { get; set; }
    }
}
