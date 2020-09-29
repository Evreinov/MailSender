using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace MailSender.lib
{
    public class SmtpSender
    {
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public void SendMail(string SenderAddress, string RecipientAddress, string Subject, string Body)
        {
            MailAddress from = new MailAddress(SenderAddress);
            MailAddress to = new MailAddress(RecipientAddress);

            // Создать письмо
            using (MailMessage message = new MailMessage(from, to))
            {
                message.Subject = Subject;
                message.Body = Body;
                // Создать клиент SMTP и отправить письмо
                using (SmtpClient client = new SmtpClient(ServerAddress, ServerPort))
                {
                    client.EnableSsl = UseSSL;
                    client.Credentials = new NetworkCredential(Login, Password);
                    try
                    {
                        client.Send(message);
                    }
                    catch (SmtpException e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                    
                }
            }
        }
    }
}
