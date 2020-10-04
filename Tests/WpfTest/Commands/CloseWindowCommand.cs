using System.Linq;
using System.Windows;
using WpfTest.Commands.Base;

namespace WpfTest.Commands
{
    class CloseWindowCommand : Command
    {
        protected override void Execute(object parameter)
        {
            var window = parameter as Window;

            if (window is null)
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

            if (window is null)
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

            window?.Close();
        }
    }
}
