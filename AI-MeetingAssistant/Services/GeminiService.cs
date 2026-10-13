using System.Text.Json;

namespace AI_MeetingAssistant.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeminiService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private async Task<string> CallGeminiAsync(string prompt)
        {
            var apiKey = _configuration["Gemini:ApiKey"];

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.7-flash:generateContent?key={apiKey}",
                requestBody);

            var rawJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API fel ({response.StatusCode}): {rawJson}");
            }

            using var doc = JsonDocument.Parse(rawJson);
            try
            {
                var text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? string.Empty;
            }
            catch (Exception)
            {
                return "Kunde inte tolka texten ur AI-svaret.";
            }
        }

        public async Task<string> SummariseMeeting(string prompt)
        {
            return await CallGeminiAsync(prompt);
        }

        public async Task<string> CreateAgenda(string prompt)
        {
            return await CallGeminiAsync(prompt);
        }

        public async Task<string> WriteAProffesionalInvite(string prompt)
        {
            return await CallGeminiAsync(prompt);
        }
    }
}