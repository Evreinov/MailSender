using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace WpfMailSender
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
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
                string login = LoginEdit.Text; 
                var password = PasswordEdit.SecurePassword;

                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = true;

                client.Send(message);
            }
        }
    }
}
