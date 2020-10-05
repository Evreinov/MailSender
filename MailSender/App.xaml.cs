using MailSender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MailSender
{
    public partial class App
    {
        private static IServiceProvider _Services;

        public static IServiceProvider Service => _Services ??= GetServices().BuildServiceProvider();

        private static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();
            InitializeServices(services);
            return services;
        }

        private static void InitializeServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
        }
    }
}
