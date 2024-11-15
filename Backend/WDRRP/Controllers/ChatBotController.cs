using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WDRRP.Dtos;
using WDRRP.Services;

namespace WDRRP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatbotService;

        public ChatBotController(IChatBotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            try
            {
                var response = await _chatbotService.GetChatbotResponse(request.Message);
                return Ok(new { response });
            }
            catch (HttpRequestException)
            {
                return StatusCode(500, "Failed to get a response from the chatbot.");
            }
        }
    }
}
