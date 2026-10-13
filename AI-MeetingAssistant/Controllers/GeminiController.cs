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

            const string systemPrompt = "Du är en Ai som har som uppdrag att summera ett möte som ett proffs :), all text efter detta är det du ska summera:";
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

            const string systemPrompt = "Du är en Ai som har som uppdrag att skapa en agenda inför ett möte som ett proffs :), all text efter detta är det du ska använda:";
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

            const string systemPrompt = "Du är en Ai som har som uppdrag att skapa en proffesionel inbjudan till ett möte :), all text efter detta är det du ska använda:";
            string prompt = systemPrompt + request.InputPrompt;

            var result = await _geminiService.WriteAProffesionalInvite(prompt);
            return Ok(new { result });
        }
    }
}