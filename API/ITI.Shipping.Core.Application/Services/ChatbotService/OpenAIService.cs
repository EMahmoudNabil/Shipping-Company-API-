using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using ITI.Shipping.Core.Application.Abstraction.chat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAIConfiguration _config;
    private readonly ILogger<OpenAIService> _logger;

    public OpenAIService(
        HttpClient httpClient,
        IOptions<OpenAIConfiguration> options,
        ILogger<OpenAIService> logger)
    {
        _httpClient = httpClient;
        _config = options.Value;
        _logger = logger;

        if (string.IsNullOrEmpty(_config.ApiKey))
        {
            throw new ArgumentNullException("OpenAI API Key is not configured");
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _config.ApiKey);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<OpenAICompletionResponse> GetCompletionAsync(string prompt)
    {
        try
        {
            var requestBody = new
            {
                model = _config.Model,
                prompt = prompt,
                max_tokens = _config.MaxTokens,
                temperature = _config.Temperature
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "https://api.openai.com/v1/completions",
                content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OpenAICompletionResponse>(responseString);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling OpenAI API");
            throw new ApplicationException("Failed to get response from OpenAI", ex);
        }
    }
}