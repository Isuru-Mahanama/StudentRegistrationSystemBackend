using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using StudentRegistrationSystem.Helper;
using StudentRegistrationSystem.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        

        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(emailSettings.Email);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(emailSettings.Host, emailSettings.Port,SecureSocketOptions.StartTls);
                smtp.Authenticate(emailSettings.Email,emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                // Send the email
               

                // Disconnect from the server
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the error)
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow the exception for higher-level error handling
            }
        }
    }
}
