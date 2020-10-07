using MailSender.lib.Interface;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MailSender.lib.Services
{
    public class DebugMailService : IMailService
    {
        public IMailSender GetSender(string Address, int Port, bool UseSSL, string Login, string Password) =>
            new DebugMailSender(Address, Port, UseSSL, Login, Password);

        private class DebugMailSender : IMailSender
        {
            private readonly string address;
            private readonly int port;
            private readonly bool useSSL;
            private readonly string login;
            private string  password;

            public DebugMailSender(string address, int port, bool useSSL, string login, string password)
            {
                this.address = address;
                this.port = port;
                this.useSSL = useSSL;
                this.login = login;
                this.password = password;
            }

            public void Send(string From, string To, string Title, string Message)
            {
                Debug.WriteLine("Почтовый сервер {0}:{1}(ssl:{2})[login:{3} - pass:{4}]",
                    address, port, useSSL, login, password);
                Debug.WriteLine("Отправка письма от:{0} к: {1}\r\n\t{2}\r\n{3}", 
                    From, To, Title, Message);
            }
        }


    }
}
