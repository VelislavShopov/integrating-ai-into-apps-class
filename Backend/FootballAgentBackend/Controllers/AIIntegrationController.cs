using Microsoft.AspNetCore.Mvc;

namespace FootballAgentBackend.Controllers
{
    [ApiController]
    [Route("ai")]
    public class AIIntegrationController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}
