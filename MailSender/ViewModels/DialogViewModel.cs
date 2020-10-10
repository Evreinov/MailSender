using MailSender.Interfaces;

namespace MailSender.ViewModels
{
    public class DialogViewModel<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public T DialogResult { get; set; }

        public DialogViewModel() : this(string.Empty, string.Empty) { }
        public DialogViewModel(string title) : this(title, string.Empty) { }
        public DialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
