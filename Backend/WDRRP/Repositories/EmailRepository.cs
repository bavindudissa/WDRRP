using System;
using System.Net;
using System.Net.Mail;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class EmailRepository : IEmailService
{
    public async Task<bool> SendEmailAsync(string email, string subject, string body)
    {
         // Sender's email address and password
        string fromEmail = "tharushilakshanifyp24@gmail.com";
        string password = "wjlswancaaeroqjn";
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;
        SmtpClient smtpClient = new SmtpClient(smtpAddress)
        {
            Port = portNumber,
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = enableSSL,
        };
        // Create email message
        MailMessage mailMessage = new MailMessage(fromEmail, email)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        try
        {
            // Send email
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email: " + ex.Message);
            return false;
        }
    }
}
