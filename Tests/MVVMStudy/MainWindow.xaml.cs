using MVVMStudy.ViewModels;

namespace MVVMStudy
{
    public partial class MainWindow
    {
        public MainWindowsViewModel ViewModel { get; set; } = new MainWindowsViewModel();
        public MainWindow() => InitializeComponent();

    }
}
