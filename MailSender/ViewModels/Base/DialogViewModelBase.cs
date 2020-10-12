using MailSender.Interfaces;

namespace MailSender.ViewModels.Base
{
    public class DialogViewModelBase<T>
    {
        public string Title { get; set; }
        public object Object { get; set; }
        public T DialogResult { get; set; }

        public DialogViewModelBase() : this(string.Empty, null) { }
        public DialogViewModelBase(string title) : this(title, null) { }
        public DialogViewModelBase(string title, object obj)
        {
            Title = title;
            Object = obj;
        }

        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
