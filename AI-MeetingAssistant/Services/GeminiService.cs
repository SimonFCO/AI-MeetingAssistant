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

        public async Task<string> SummariseMeeting(string prompt)
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
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent?key={apiKey}",
                requestBody);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateAgenda(string prompt)
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
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.7-flash:generateContent?key={apiKey}",
                requestBody);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> WriteAProffesionalInvite(string prompt)
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
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.7-flash:generateContent?key={apiKey}",
                requestBody);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
