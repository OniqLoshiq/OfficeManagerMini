using OMM.Services.SendGrid.Common;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace OMM.Services.SendGrid
{
    public class SendGird : ISendGrid
    {
        public async Task<bool> SendRegistrationMailAsync(string recipientEmail, string recipientName, string password)
        {
            var recipient = CreateAddress(recipientEmail, recipientName);
            var sender = CreateAddress(Constants.DEFAULT_SENDER_EMAIL, Constants.DEFAULT_SENDER_NAME);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = sender;
            var subject = Constants.REGISTRATION_SUBJECT;
            var to = recipient;
            var plainTextContent = "";
            var htmlContent = string.Format(Constants.HTML_TEXT, recipientEmail, password);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode.ToString().ToLower() == "accepted";
        }

        private EmailAddress CreateAddress(string email, string senderName)
        {
            string nameWithPrefix = GetNameWithPrefix(senderName);

            return new EmailAddress(email, nameWithPrefix);
        }

        private string GetNameWithPrefix(string name)
        {
            return string.Format(Constants.PLATFORM_PREFIX, name);
        }
    }
}
