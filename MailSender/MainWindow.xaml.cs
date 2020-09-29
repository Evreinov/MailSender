using System.Net.Mail;
using System.Windows;
using MailSender.lib;
using MailSender.Models;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void OnSendButtonClick(object Sender, System.Windows.RoutedEventArgs E)
        {
            if (!(SendersList.SelectedItem is Sender sender)) return;
            if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(ServersList.SelectedItem is Server server)) return;
            if (!(MailsList.SelectedItem is Mail mail)) return;

            MailSenderService sent_service = new MailSenderService
            {
                ServerAddress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password
            };

            try
            {
                sent_service.SendMail(sender.Address, recipient.Address, mail.Subject, mail.Body);
            }
            catch (SmtpException e)
            {
                MessageBox.Show(
                    "Ошибка при отправке почты " + e.Message, 
                    "Ошибка",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
    }
}
