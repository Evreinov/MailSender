using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Interface
{
    public interface IMailSender
    {
        void Send(string From, string To, string Title, string Message);
    }
}
