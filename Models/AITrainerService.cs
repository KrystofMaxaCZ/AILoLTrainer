using System.Text.Json;

namespace LoLAnalyzer.Models;

public class AITrainerService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly string _apiKey;
    private readonly string _model;
    
    public AITrainerService(IConfiguration configuration)
    {
        _apiKey = configuration["Gemini:ApiKey"]
                  ?? throw new InvalidOperationException("Gemini API key is not configured.");
        _model = configuration["Gemini:Model"] ?? "gemini-3.6-flash";
    }
    
    public async Task<string> CallTheAI(string promptText)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_model}:generateContent";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = promptText }
                    }
                }
            }
        };
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = JsonContent.Create(requestBody)
        };

        request.Headers.Add("X-Goog-Api-Key", _apiKey);

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request);
        }
        catch (HttpRequestException)
        {
            return "Could not reach the AI service. Check your internet connection and try again.";
        }

        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            return "The AI request limit is currently exhausted. Please try again shortly.";
        }

        if (!response.IsSuccessStatusCode)
        {
            // Covers 503, 500, and any other non-success status without crashing the app
            return $"The AI service is temporarily unavailable (status {(int)response.StatusCode}). Please try again in a moment.";
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        string result = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? "";

        return result;
    }
}