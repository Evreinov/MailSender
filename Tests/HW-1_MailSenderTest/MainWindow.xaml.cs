using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HW_1_MailSenderTest
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void BtnSend_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!Validate())
                return;
            MailAddress to = new MailAddress(TxtTo.Text);
            MailAddress from = new MailAddress(TxtFrom.Text, TxtUserName.Text);

            using MailMessage message = new MailMessage(from, to)
            {
                Subject = TxtSubject.Text,
                Body = TxtBody.Text
            };

            using (SmtpClient client = new SmtpClient(TxtSmtpHost.Text))
            {

                string login = TxtFrom.Text;
                var password = Password.SecurePassword;

                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = (bool)CheckSSL.IsChecked;
                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), "Ошибка отправки сообщения");
                }
            }
        }

        /// <summary>
        /// Проверка формы на заполнение.
        /// </summary>
        /// <returns>false - если обязательные поля не заполнены.</returns>
        private bool Validate()
        {
            bool flag = true;
            StringBuilder sb= new StringBuilder("Необходимо заполнить следующие поля:\n");
            if (TxtTo.Text == string.Empty)
            {
                flag = false;
                sb.Append("- Кому\n");
            }
            if (TxtFrom.Text == string.Empty)
            {
                flag = false;
                sb.Append("- От кого\n");
            }
            if (TxtSmtpHost.Text == string.Empty)
            {
                flag = false;
                sb.Append("- Адрес SMTP сервера\n");
            }
            if (Password.SecurePassword.Length == 0)
            { 
                flag = false;
                sb.Append("- Пароль от почтового ящика пользователя\n");
            }
            if (!flag)
                System.Windows.MessageBox.Show(sb.ToString(), "Ошибка заполнения");
            return flag;
        }
    }
}
