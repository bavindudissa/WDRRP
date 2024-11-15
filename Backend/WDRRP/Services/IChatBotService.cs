using System;

namespace WDRRP.Services;

public interface IChatBotService
{
    Task<string?> GetChatbotResponse(string message);
}
