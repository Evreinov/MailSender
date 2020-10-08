using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    class LocatorViewModel
    {
        public MainWindowViewModel MainWindowModel => App.Service.GetRequiredService<MainWindowViewModel>();
    }
}
