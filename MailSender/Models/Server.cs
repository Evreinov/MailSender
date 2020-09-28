using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    class Server
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public int Port { get; set; } = 25;
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
