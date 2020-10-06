using MailSender.lib.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender.lib.Services
{
    internal class SmtpMailSender : IMailSender
    {
        private string address;
        private int port;
        private bool useSSL;
        private string login;
        private string password;

        public SmtpMailSender(string address, int port, bool useSSL, string login, string password)
        {
            this.address = address;
            this.port = port;
            this.useSSL = useSSL;
            this.login = login;
            this.password = password;
        }

        public void Send(string From, string To, string Title, string Message)
        {
            MailAddress from = new MailAddress(From);
            MailAddress to = new MailAddress(To);

            // Создать письмо
            using (MailMessage message = new MailMessage(from, to))
            {
                message.Subject = Title;
                message.Body = Message;
                // Создать клиент SMTP и отправить письмо
                using (SmtpClient client = new SmtpClient(address, port))
                {
                    client.EnableSsl = useSSL;
                    client.Credentials = new NetworkCredential(login, password);
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
