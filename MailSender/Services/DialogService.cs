using MailSender.ViewModels;
using MailSender.Interfaces;

namespace MailSender.Services
{
    class DialogService
    {
        public T OpenDialog<T>(DialogViewModel<T> viewModel)
        {
            IDialogWindow window = new SenderEditDialog();
            window.DataContext = viewModel;
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
