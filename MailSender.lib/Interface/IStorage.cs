using MailSender.lib.Models;
using System.Collections.Generic;

namespace MailSender.lib.Interface
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }
        void Load();
        void SaveChanges();
    }

    public interface IServersStorage : IStorage<Server> { }
    public interface ISendersStorage : IStorage<Sender> { }
    public interface IRecipientsStorage : IStorage<Recipient> { }
    public interface IMailsStorage : IStorage<Mail> { }
}
