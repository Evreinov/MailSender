using System.Diagnostics;
using System.Net.Mail;
using System.Windows;
using System.Linq;
using MailSender.lib;
using MailSender.Models;
using MailSender.Data;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void OnAddServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ServerEditDialog.Create(
                out var name, 
                out var address, 
                out var port, 
                out var ssl, 
                out var description,
                out var login,
                out var password)) 
                return;
            var server = new Server
            {
                Id = TestData.Servers.DefaultIfEmpty().Max(s => s.Id) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };

            TestData.Servers.Add(server);
            ServersList.ItemsSource = null;
            ServersList.ItemsSource = TestData.Servers;
            ServersList.SelectedItem = server;
        }

        private void OnEditServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(ServersList.SelectedItem is Server server)) return;

            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;

            if (!ServerEditDialog.ShowDialog(
                "Редактирование сервера",
                ref name,
                ref address,
                ref port,
                ref ssl,
                ref description,
                ref login,
                ref password)) 
                return;

            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Description = description;
            server.Login = login;
            server.Password = password;

            ServersList.ItemsSource = null;
            ServersList.ItemsSource = TestData.Servers;
        }

        private void OnDeleteServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(ServersList.SelectedItem is Server server)) return; 

            TestData.Servers.Remove(server);
            ServersList.ItemsSource = null;
            ServersList.ItemsSource = TestData.Servers;
            ServersList.SelectedItem = TestData.Servers.FirstOrDefault();
        }

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
