using System;
using Emailing.Service.API.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Emailing.Service.API.Services
{
        public class MessagingService : IMessagingService
        {
                private static string apiKey => Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

                public MessagingService()
                {
                }

                public async System.Threading.Tasks.Task SendEmail(string to, string subject, string htmlContent)
                {
                        var sendGridClient = new SendGridClient(apiKey);
                        var fromEmail = new EmailAddress("karimhassan.abdelrahman@gmail.com", "peer review tool");
                        var toEmail = new EmailAddress(to);
                        var Emailsubject = subject;
                        var EmailhtmlContent = htmlContent;
                        var plainTextContent = "";
                        var email = MailHelper.CreateSingleEmail(fromEmail, toEmail, Emailsubject, plainTextContent, EmailhtmlContent);

                        await sendGridClient.SendEmailAsync(email);
                }
        }
}