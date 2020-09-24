using System;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace ConsoleMailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            // Объявить переменные содержащие e-mail'ы отправителя и получателя.
            MailAddress from = new MailAddress("damian.ev@ya.ru", "Дмитрий Евреинов");
            MailAddress to = new MailAddress("damian.ev@ya.ru");

            // Создать письмо
            using MailMessage message = new MailMessage(from, to)
            {
                Subject = "Тестовое сообщение",
                Body = $"Текст письма: {DateTime.Now}"
            };

            // Создать клиент SMTP и отправить письмо
            using (SmtpClient client = new SmtpClient("smtp.yandex.ru"))
            {
                string login, password;

                // Файл с логином и паролем (включен в .gitignore)
                using (StreamReader sr = new StreamReader(@"..\..\..\..\..\secure.txt"))
                {
                    login = sr.ReadLine();
                    password = sr.ReadLine();
                }

                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = true;

                client.Send(message);
            }

            Console.WriteLine("Письмо отправлено.");
            Console.ReadKey();
        }
    }
}
