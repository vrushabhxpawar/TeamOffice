using DemoDownloadPage.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DemoDownloadPage.Services
    {
    public class MailService
        {

        public readonly MailParameters _settings;
        public MailService (IOptions<MailParameters> options)
            {
            _settings = options.Value;
            }

        public async Task<bool> SendEmailAsync (
            string toEmail,
            string subject,
            string templateName,
            Dictionary<string, string> placeholders)
            {
            try
                {
                MailMessage msg = new MailMessage(
                    new MailAddress(_settings.senderMail, "TeamOffice"),
                    new MailAddress(toEmail));

                string templatePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "EmailTemplates",
                    $"{templateName}.html");

                string body = await File.ReadAllTextAsync(templatePath);

                foreach (var item in placeholders)
                    {
                    body = body.Replace($"{{{{{item.Key}}}}}", item.Value);
                    }

                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(_settings.smtp, Convert.ToInt32(_settings.port)))
                    {
                    smtp.Credentials = new NetworkCredential(_settings.senderMail, _settings.password);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(msg);
                    }

                return true;
                }
            catch
                {
                return false;
                }
            }
        }
    }
