using System.Windows;
using System.Windows.Input;
using WpfTest.Commands;
using WpfTest.ViewModels.Base;

namespace WpfTest.ViewModels
{
    class ShowDialogViewModel : ViewModel
    {
        private ICommand _ShowDialogCommand;

        public ICommand ShowDialogCommand => _ShowDialogCommand ??= new LambdaCommand(OnShowDialogCommandExecute);

        private void OnShowDialogCommandExecute(object parameter)
        {
            string message = parameter as string ?? "Hello world!";
            MessageBox.Show(message);
        }
    }
}
