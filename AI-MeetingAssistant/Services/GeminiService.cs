using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
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
            // This will be true when a request succeeds
            bool success = false;

            string[] aiModels = {"gemini-3.8-flash", "gemini-3.7-flash", "gemini-3.6-flash", "gemini-3.5-flash", "gemini-3.1-flash-lite" };

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
                },
                generationConfig = new
                {
                    thinkingConfig = new
                    {
                        thinkingLevel = "LOW"
                    }
                }
            };
            var response = await _httpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.1-flash-lite:generateContent?key={apiKey}",
            requestBody);

            var rawJson = await response.Content.ReadAsStringAsync();
            int loops = 1;
            foreach(var model in aiModels)
            {
          
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {

                        Console.WriteLine($"[DEBUG]:{model}  var redan använd testar en annan");
                        response = await _httpClient.PostAsJsonAsync(
                        $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}",
                        requestBody);

                        rawJson = await response.Content.ReadAsStringAsync();
                        loops++;
                        if(loops == 5)
                        {
                            loops = 0;
                        }
                        await Task.Delay(10000);
                    }
                    else
                    {
                        throw new Exception($"Gemini API fel ({response.StatusCode}): {rawJson}");
                    }
                }else
                {
                    success = true;
                }
            }
            loops = 0;
           
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