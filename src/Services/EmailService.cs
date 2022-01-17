using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace src.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            var apiKey = ConfigurationManager.AppSettings["SendGridAPI"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("zorgmaatschapdenhaag@gmail.com", "ZMDH");
            var subject = message.Subject;
            var to = new EmailAddress("zorgmaatschapdenhaag@gmail.com", "ZMDH");
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            // Send the email.
            if (client != null)
            {
                await client.SendEmailAsync(msg);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}
