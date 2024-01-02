using System.Net.Mail;
using System.Net;
using LmsApi.IService;

namespace LmsApi.Service;

public class EmailService : IEmailService
{
    readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string to, string subject, string body)
    {
        string smtpHost = "smtp.gmail.com";
        int smtpPort = 587;
        string smtpUsername = _configuration["Email"];
        string smtpPassword = _configuration["EmailPassword"];
        bool enableSsl = true;

        using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = enableSsl;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
