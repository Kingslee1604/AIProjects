using Newtonsoft.Json;

namespace ChatGPTOpenAI
{
    public class OpenAiResponse
    {
        [JsonProperty("choices")]
        public List<Choice> choices { get; set; }
    }
}
