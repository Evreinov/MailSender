using MailSender.ViewModels;

namespace MailSender.Interfaces
{
    public interface IDialogService
    {
        T OpenDialog<T>(DialogViewModel<T> ViewModel);
    }
}
