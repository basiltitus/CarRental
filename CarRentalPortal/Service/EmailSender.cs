using CarRentalPortal.Service;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagementSystem.Service
{
    public class EmailSender
    {
        private readonly MyOptions _options;
        public EmailSender(MyOptions options)
        {
            _options = options;
        }
        public Task SendEmailAsync(string email,string subject,string htmlMessage)
        {
            return Execute(_options.SendgridKey, subject, htmlMessage, email);
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
                using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
                {
                    var currentTime = DateTime.Now;
                    writetext.WriteLine(currentTime + " : " + e.Message.ToString());
                }
                return null;
            }
        }
    }
}
