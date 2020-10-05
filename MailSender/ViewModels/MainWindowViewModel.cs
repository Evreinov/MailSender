using MailSender.Commands;
using MailSender.Data;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
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

        private LambdaCommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecute);
        private void OnLoadDataCommandExecute(object parameter)
        {
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Mails = new ObservableCollection<Mail>(TestData.Mails);
        }
    }
}
