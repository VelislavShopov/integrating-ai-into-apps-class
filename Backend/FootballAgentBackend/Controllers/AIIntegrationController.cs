using FootballAgentBackend.AIClient;
using FootballAgentBackend.DTOs;
using Google.GenAI;
using Microsoft.AspNetCore.Mvc;

namespace FootballAgentBackend.Controllers
{
    [ApiController]
    [Route("ai")]
    public class AIIntegrationController(IAIClient client) : ControllerBase
    {
        private IAIClient _aiClient = client;


        [HttpPost]
        public async Task<ActionResult<AIResponseDTO>> Post([FromBody] QueryDTO queryDTO)
        {
            var response = await _aiClient.GetResponse(queryDTO.Query);

            return Ok(response.Candidates[0].Content.Parts[0].Text);
        }
    }
}
