using MailSender.Commands;
using MailSender.lib.Interface;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly IMailService _MailService;
        private readonly IServersStorage _ServersStorage;
        private readonly ISendersStorage _SendersStorage;
        private readonly IRecipientsStorage _RecipientsStorage;
        private readonly IMailsStorage _MailsStorage;

        public StatisticViewModel Statistic { get; } = new StatisticViewModel();

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

        private Sender _SelectedSender;
        public Sender SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }

        private Recipient _SelectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        private Mail _SelectedMail;
        public Mail SelectedMail
        {
            get => _SelectedMail;
            set => Set(ref _SelectedMail, value);
        }

        public MainWindowViewModel(IMailService MailService, 
            IServersStorage ServerStorage, 
            ISendersStorage SendersStorage, 
            IRecipientsStorage RecipientsStorage, 
            IMailsStorage MessagesStorage)
        {

            _MailService = MailService;
            _ServersStorage = ServerStorage; 
            _SendersStorage = SendersStorage; 
            _RecipientsStorage = RecipientsStorage; 
            _MailsStorage = MessagesStorage;
        }

        #region Command

        #region LoadDataCommand
        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommand(OnLoadDataCommandExecute);
        private void OnLoadDataCommandExecute(object parameter)
        {
            _ServersStorage.Load();
            _RecipientsStorage.Load();
            _SendersStorage.Load();
            _MailsStorage.Load();
            Servers = new ObservableCollection<Server>(_ServersStorage.Items);
            Senders = new ObservableCollection<Sender>(_SendersStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_RecipientsStorage.Items);
            Mails = new ObservableCollection<Mail>(_MailsStorage.Items);
        }
        #endregion

        #region SaveDataCommand
        private ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand
            ??= new LambdaCommand(OnSaveDataCommandExecute);
        private void OnSaveDataCommandExecute(object parameter)
        {
            _ServersStorage.SaveChanges();
            _SendersStorage.SaveChanges();
            _RecipientsStorage.SaveChanges();
            _MailsStorage.SaveChanges();
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
            _ServersStorage.Items.Add(server);
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
            _ServersStorage.Items.Remove(server);
            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
        }
        #endregion

        #region SendMailCommand
        private ICommand _SendMailCommand;
        public ICommand SendMailCommand => _SendMailCommand
            ??= new LambdaCommand(OnSendMailCommandExecute, CanSendMailCommandExecute);
        private bool CanSendMailCommandExecute(object obj)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedRecipient is null) return false;
            if (SelectedMail is null) return false;
            return true;
        }
        private void OnSendMailCommandExecute(object obj)
        {
            var server = SelectedServer;
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var mail = SelectedMail;

            var mail_sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            mail_sender.Send(sender.Address, recipient.Address, mail.Subject, mail.Body);

            Statistic.MessageSended();
        }

        #endregion

        #endregion
    }
}
