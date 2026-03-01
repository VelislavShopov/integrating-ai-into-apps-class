using Google.GenAI;
using Google.GenAI.Types;

namespace FootballAgentBackend.AIClient
{
    public interface IAIClient
    {
        public Task<GenerateContentResponse> GetResponse(string userPrompt);
    }
}
