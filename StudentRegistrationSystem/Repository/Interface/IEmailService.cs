using StudentRegistrationSystem.Helper;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
