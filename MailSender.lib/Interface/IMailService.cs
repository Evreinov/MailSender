using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Interface
{
    public interface IMailService
    {
        IMailSender GetSender(string Address, int Port, bool UseSSL, string Login, string Password);
    }
}
