using MailSender.lib.Interface;

namespace MailSender.lib.Services
{
    public class SmtpMailService : IMailService
    {
        public IMailSender GetSender(string Address, int Port, bool UseSSL, string Login, string Password)
        {
            return new SmtpMailSender(Address, Port, UseSSL, Login, Password);
        }
    }
}
