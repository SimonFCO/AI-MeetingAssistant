using AI_MeetingAssistant.Services;
using Microsoft.AspNetCore.Mvc;

namespace AI_MeetingAssistant.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiService _geminiService;

        public GeminiController(
            GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("summarize")]
        public async Task<IActionResult> summarize(string inputPrompt)
        {
            if (string.IsNullOrWhiteSpace(inputPrompt))
            {
                return BadRequest("Måste innehålla någon text.");
            }

            const string systemPrompt = "Du är en Ai som har som uppdrag att summera ett möte som ett proffs :), all text efter detta är det du ska summera:";

            string prompt = systemPrompt + inputPrompt;

            var result = await _geminiService
                .SummariseMeeting(prompt);

            return Ok(result);
        }

        [HttpPost("agenda")]
        public async Task<IActionResult> agenda(string inputPrompt)
        {

            if (string.IsNullOrWhiteSpace(inputPrompt))
            {
                return BadRequest("Måste innehålla någon text.");
            }

            const string systemPrompt = "Du är en Ai som har som uppdrag att skapa en agenda inför ett möte som ett proffs :), all text efter detta är det du ska använda:";

            string prompt = systemPrompt + inputPrompt;

            var result = await _geminiService
                .CreateAgenda(prompt);

            return Ok(result);
        }

        [HttpPost("invitation")]
        public async Task<IActionResult> invitation(string inputPrompt)
        {
            if (string.IsNullOrWhiteSpace(inputPrompt))
            {
                return BadRequest("Måste innehålla någon text.");
            }

            const string systemPrompt = "Du är en Ai som har som uppdrag att skapa en proffesionel inbjudan till ett möte :), all text efter detta är det du ska använda:";

            string prompt = systemPrompt + inputPrompt;

            var result = await _geminiService
                .WriteAProffesionalInvite(prompt);

            return Ok(result);
        }
    }
}
