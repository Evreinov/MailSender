using System.Diagnostics;
using System.Net.Mail;
using System.Windows;
using MailSender.lib;
using MailSender.Models;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void OnSendButtonClick(object _sender, System.Windows.RoutedEventArgs e)
        {
            if (!(SendersList.SelectedItem is Sender sender)) return;
            if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(ServersList.SelectedItem is Server server)) return;
            if (!(MailsList.SelectedItem is Mail mail)) return;

            SmtpSender sent_service = new SmtpSender
            {
                ServerAddress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password
            };

            try
            {
                var timer = Stopwatch.StartNew();
                sent_service.SendMail(sender.Address, recipient.Address, mail.Subject, mail.Body);
                timer.Stop();
                MessageBox.Show(
                    $"Почта отправленна успешно за {timer.Elapsed.TotalSeconds:0.##}",
                    "Отправка почты",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(
                    "Ошибка при отправке почты " + ex.Message, 
                    "Отправка почты",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
    }
}
