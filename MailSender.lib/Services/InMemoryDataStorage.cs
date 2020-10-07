using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MailSender.lib.Interface;
using MailSender.lib.Models;

namespace MailSender.lib.Services
{
    public class InMemoryDataStorage : IServersStorage, ISendersStorage, IRecipientsStorage, IMailsStorage
    {
        private readonly IEncryptorService _EncryptorService;

        public InMemoryDataStorage(IEncryptorService EncryptorService)
        {
            _EncryptorService = EncryptorService;
        }

        public ICollection<Server> Servers { get; set; } = new List<Server>();
        public ICollection<Sender> Senders { get; set; } = new List<Sender>();
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
        public ICollection<Mail> Mails { get; set; } = new List<Mail>();

        ICollection<Server> IStorage<Server>.Items => Servers;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Mail> IStorage<Mail>.Items => Mails;

        public void Load()
        {
            Debug.WriteLine("Вызвана процедура загрузки данных.");

            if (Servers is null || Servers.Count == 0)
                Servers = Enumerable.Range(1, 5)
                    .Select(i => new Server
                    {
                        Id = i,
                        Name = $"Имя {i}",
                        Address = $"smtp.server{i}",
                        Login = $"Пользователь {i}",
                        Password = _EncryptorService.Encrypt($"Пароль{i}", "Пароль!"),
                        UseSSL = i % 2 == 0
                    })
                    .ToList();

            if (Senders is null || Senders.Count == 0)
                Senders = Enumerable.Range(1, 20)
                    .Select(i => new Sender
                    {
                        Id = i,
                        Name = $"Отправитель {i}",
                        Address = $"sender_{i}@server.ru",
                    })
                    .ToList();

            if (Recipients is null || Recipients.Count == 0)
                Recipients = Enumerable.Range(1, 20)
                    .Select(i => new Recipient
                    {
                        Id = i,
                        Name = $"Получатель {i}",
                        Address = $"recipient_{i}@server.ru",
                    })
                    .ToList();

            if (Mails is null || Mails.Count == 0)
                Mails = Enumerable.Range(1, 20)
                    .Select(i => new Mail
                    {
                        Id = i,
                        Subject = $"Заголовок {i}",
                        Body = $"Текст сообщения {i}."
                    })
                    .ToList();
        }
        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}
