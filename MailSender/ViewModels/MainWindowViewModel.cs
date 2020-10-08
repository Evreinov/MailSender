using MailSender.Commands;
using MailSender.lib.Interface;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    partial class MainWindowViewModel : ViewModelBase
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
    }
}
