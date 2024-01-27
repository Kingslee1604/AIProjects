using Newtonsoft.Json;

namespace ChatGPTOpenAI
{
    public class UserMessageRequest
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
