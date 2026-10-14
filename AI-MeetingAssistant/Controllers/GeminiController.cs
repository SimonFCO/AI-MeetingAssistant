using AI_MeetingAssistant.Services;
using Microsoft.AspNetCore.Mvc;

namespace AI_MeetingAssistant.Controllers
{
    public record PromptRequest(string InputPrompt);

    [ApiController]
    [Route("api/ai")]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiService _geminiService;

        public GeminiController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("summarize")]
        public async Task<IActionResult> summarize([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.InputPrompt))
            {
                return BadRequest("Måste innehålla någon text.");
            }

            const string systemPrompt = "Du är en Ai som har som uppdrag att summera ett möte som ett proffs :), all text efter detta är det du ska summera det ska innehålla ett datum och en titel sen main body av text, skriv i klar text ingen markdown:";
            string prompt = systemPrompt + request.InputPrompt;

            var result = await _geminiService.SummariseMeeting(prompt);
            return Ok(new { result });
        }

        [HttpPost("agenda")]
        public async Task<IActionResult> agenda([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.InputPrompt))
            {
                return BadRequest("Måste innehålla någon text.");
            }

            const string systemPrompt = "Du är en Ai som har som uppdrag att skriva en agenda som ett proffs :), all text efter detta är det du ska skapa en agenda med. det ska innehålla ett datum och en titel sen main body av text, skriv i klar text ingen markdown:";
            string prompt = systemPrompt + request.InputPrompt;

            var result = await _geminiService.CreateAgenda(prompt);
            return Ok(new { result });
        }

        [HttpPost("invitation")]
        public async Task<IActionResult> invitation([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.InputPrompt))
            {
                return BadRequest("Måste innehålla någon text.");
            }

            const string systemPrompt = "Du är en Ai som har som uppdrag att skriva en injudan som ett proffs :), all text efter detta är det du ska skapa en inbjudan med. det ska innehålla ett datum och en titel sen main body av text, skriv i klar text ingen markdown:";
            string prompt = systemPrompt + request.InputPrompt;

            var result = await _geminiService.WriteAProffesionalInvite(prompt);
            return Ok(new { result });
        }
    }
}