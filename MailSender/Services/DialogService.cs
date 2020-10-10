using MailSender.ViewModels;
using MailSender.Interfaces;
using MailSender.ViewModels.Base;

namespace MailSender.Services
{
    class DialogService : IDialogService
    {
        public T OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new SenderEditDialog();
            window.DataContext = viewModel;
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
