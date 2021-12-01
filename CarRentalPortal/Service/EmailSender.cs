using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagementSystem.Service
{
    public class EmailSender
    {
        
        public Task SendEmailAsync(string email,string subject,string htmlMessage)
        {
            return Execute("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", subject, htmlMessage, email);
        }

        private Task Execute(string sendgridKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(sendgridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("basiltitus88@gmail.com", "Car Rental Portal"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));
            try
            {
                return client.SendEmailAsync(msg);
            }catch(Exception e)
            {
                return null;
            }
        }
    }
}
