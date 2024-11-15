using System;
using System.Text;
using Newtonsoft.Json;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class ChatBotRepository : IChatBotService
{
        private readonly HttpClient _httpClient;

        public ChatBotRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    public async Task<string?> GetChatbotResponse(string message)
    {
        // Prepare the JSON payload with the user message
        var payload = new { message = message };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // Send the request to the chatbot API
        var response = await _httpClient.PostAsync("http://127.0.0.1:5000/chatbot", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Failed to get a response from the chatbot API.");
        }

        // Parse the response content
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
        return responseJson.response;  // Assuming the response has a 'response' property  
    }
}
