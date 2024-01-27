using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatGPTOpenAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string OpenAiEndpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";
        private const string ApiKey = "New Key";


        public SupportController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserMessageRequest request)
        {
            try
            {
                var response = await GetChatGptResponse(request.Message);
                var reply = response.choices[0].text;
                return Ok(new { Reply = reply });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal Server Error", Details = ex.Message });
            }
        }

        private async Task<OpenAiResponse> GetChatGptResponse(string message)
        {
            try
            {
                var requestBody = new
                {
                    prompt = message,
                    max_tokens = 150
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(OpenAiEndpoint, content);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                // Log the request and response for debugging purposes
                Console.WriteLine($"OpenAI Request: {JsonConvert.SerializeObject(requestBody)}");
                Console.WriteLine($"OpenAI Response: {responseBody}");

                return JsonConvert.DeserializeObject<OpenAiResponse>(responseBody);
            }
            catch (Exception ex)
            {
                // Log any exceptions for debugging purposes
                Console.WriteLine($"OpenAI Request Error: {ex.Message}");
                throw; // Rethrow the exception after logging
            }
        }

    }
}
