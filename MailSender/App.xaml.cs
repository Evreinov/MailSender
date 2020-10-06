using MailSender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using MailSender.lib.Interface;
using MailSender.lib.Services;

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
#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif

            // Выбираем либо этот блок
            var memory_store = new InMemoryDataStorage();
            services.AddSingleton<IServersStorage>(memory_store);
            services.AddSingleton<ISendersStorage>(memory_store);
            services.AddSingleton<IRecipientsStorage>(memory_store);
            services.AddSingleton<IMailsStorage>(memory_store);

            //либо этот. Один надо закомментировать, другой - раскомментировать
            //const string data_file_name = "MailSenderStorage.xml";
            //var file_storage = new InXmlFileDataStorage(data_file_name);
            //services.AddSingleton<IServersStorage>(file_storage);
            //services.AddSingleton<ISendersStorage>(file_storage);
            //services.AddSingleton<IRecipientsStorage>(file_storage);
            //services.AddSingleton<IMailsStorage>(file_storage);
        }
    }
}
