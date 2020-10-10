using MailSender.ViewModels.Base;

namespace MailSender.Interfaces
{
    public interface IDialogService
    {
        T OpenDialog<T>(DialogViewModelBase<T> ViewModel);
    }
}
