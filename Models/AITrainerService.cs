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

        var response = await _httpClient.SendAsync(request);
        
        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            return "Momentálně je vyčerpaný limit dotazů na AI. Zkus to prosím za chvíli.";
        }

        response.EnsureSuccessStatusCode();

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