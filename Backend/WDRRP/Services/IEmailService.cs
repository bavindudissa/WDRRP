using System;

namespace WDRRP.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string email, string subject, string body);
}
