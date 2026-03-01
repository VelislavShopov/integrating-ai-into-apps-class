using Google.GenAI;
using Google.GenAI.Types;

namespace FootballAgentBackend.AIClient
{
    public class AIClient : IAIClient
    {
        private IConfiguration _configuration;
        private GenerateContentConfig _config;

        private Client _client { get; set; }

        public AIClient(IConfiguration configuration) 
        {
            _configuration = configuration;
            _client = new Client(apiKey: configuration["FootballBackendSettings:GEMINI_API_KEY"]);

            var gooleSearchTool = new Tool
            {
                GoogleSearch = new GoogleSearch()
            };

            _config = new GenerateContentConfig() 
            {
                Tools = new List<Tool> { gooleSearchTool },
                SystemInstruction = new Content
                {
                    Parts = new List<Part>
                    {
                        new Part {
                            Text = "You are a football expert. When asked to find a player, use Google Search to identify the most accurate current professional footballer fitting the description." +
                            "Be awre that you are a Football-only assistant." +
                            "1. If the user asks about football, players, or teams, help them." +
                            "2. If the user asks about ANY other topic (cooking, politics, other sports), politely refuse and say: 'I only discuss football. How can I help you find a player?'" +
                            "3. Do not break character."
                        }
                    }
                },
                ResponseMimeType = "application/json",
                ResponseSchema = new Schema
                {
                    Type = Google.GenAI.Types.Type.Object,
                    Properties = new Dictionary<string, Schema>
                    {
                        ["AIResponse"] = new Schema
                        {
                            Type = Google.GenAI.Types.Type.String,
                            Description = "Your conversational scout report and explanation."
                        },
                        ["FirstName"] = new Schema { Type = Google.GenAI.Types.Type.String },
                        ["LastName"] = new Schema { Type = Google.GenAI.Types.Type.String }
                    }
                }
            };
        }

        public async Task<GenerateContentResponse> GetResponse(string userPrompt)
        {
            return await _client.Models.GenerateContentAsync(
                    model: "gemini-3-flash-preview",
                    contents: userPrompt,
                    config: _config
                );
        }
    }
}
