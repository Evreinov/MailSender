using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
