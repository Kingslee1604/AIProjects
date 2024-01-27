using Newtonsoft.Json;

namespace ChatGPTOpenAI
{
    public class Choice
    {
        [JsonProperty("text")]
        public string text { get; set; }
    }
}
