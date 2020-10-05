using MailSender.Commands;
using MailSender.Data;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        static readonly string __DataFileName = "TestData.xml";

        private string _Title = "Рассыльщик почты";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private ObservableCollection<Server> _Servers;
        public ObservableCollection<Server> Servers
        {
            get => _Servers;
            set => Set(ref _Servers, value);
        }

        private ObservableCollection<Sender> _Senders;
        public ObservableCollection<Sender> Senders
        {
            get => _Senders;
            set => Set(ref _Senders, value);
        }

        private ObservableCollection<Recipient> _Recipients;
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }

        private ObservableCollection<Mail> _Mails;
        public ObservableCollection<Mail> Mails
        {
            get => _Mails;
            set => Set(ref _Mails, value);
        }

        private Server _SelectedServer;
        public Server SelectedServer 
        { 
            get => _SelectedServer; 
            set => Set(ref _SelectedServer, value); 
        }

        private Server _SelectedSender;
        public Server SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }

        private Server _SelectedRecipient;
        public Server SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        private Server _SelectedMail;
        public Server SelectedMail
        {
            get => _SelectedMail;
            set => Set(ref _SelectedMail, value);
        }
        #region Command

        #region LoadDataCommand
        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand 
            ??= new LambdaCommand(OnLoadDataCommandExecute);
        private void OnLoadDataCommandExecute(object parameter)
        {
            TestData data = File.Exists(__DataFileName)
                ? TestData.LoadFromXML(__DataFileName)
                : new TestData();

            Servers = new ObservableCollection<Server>(data.Servers);
            Senders = new ObservableCollection<Sender>(data.Senders);
            Recipients = new ObservableCollection<Recipient>(data.Recipients);
            Mails = new ObservableCollection<Mail>(data.Mails);
        }
        #endregion

        #region SaveDataCommand
        private ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand 
            ??= new LambdaCommand(OnSaveDataCommandExecute);
        private void OnSaveDataCommandExecute(object parameter)
        {
            TestData data = new TestData
            {
                Servers = Servers,
                Senders = Senders,
                Recipients = Recipients,
                Mails = Mails
            };

            data.SaveToXML(__DataFileName);
        }
        #endregion

        #region CreateServerCommand
        private ICommand _CreateServerCommand;
        public ICommand CreateServerCommand => _CreateServerCommand 
            ??= new LambdaCommand(OnCreateServerCommandExecute);
        private void OnCreateServerCommandExecute(object obj)
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
                Id = Servers.DefaultIfEmpty().Max(s => s?.Id ?? 0) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };
            Servers.Add(server);
        }
        #endregion

        #region EditServerCommand
        private ICommand _EditServerCommand;
        public ICommand EditServerCommand => _EditServerCommand 
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecuted);
        private bool CanEditServerCommandExecuted(object obj) => obj is Server;
        private void OnEditServerCommandExecuted(object obj)
        {
            if (!(obj is Server server)) return;

            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;
            if (!ServerEditDialog.ShowDialog(
                "Редактировать сервер",
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
        }
        #endregion

        #region DeleteServerCommand
        private ICommand _DeleteServerCommand;
        public ICommand DelateServerCommand => _DeleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecute, CanDeleteServerCommandExecute);
        private bool CanDeleteServerCommandExecute(object obj) => obj is Server;
        private void OnDeleteServerCommandExecute(object obj)
        {
            if (!(obj is Server server)) return;
            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
        }
        #endregion

        #endregion

    }
}
