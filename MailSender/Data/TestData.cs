using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MailSender.Models;
using System.Xaml;
using MailSender.lib.Services;
using System.Xml.Serialization;
using System.IO;

namespace MailSender.Data
{
    class TestData
    {
        public static TestData LoadFromXML(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestData));
            using var file = File.OpenText(FileName);
            return (TestData)serializer.Deserialize(file);
        }

        public void SaveToXML(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestData));
            using var file = File.Create(FileName);
            serializer.Serialize(file, this);
        }

        public IList<Sender> Senders { get; set; } = Enumerable.Range(1, 20)
            .Select(i => new Sender
            {
                Id = i,
                Name = $"Отправитель {i}",
                Address = $"sender_{i}@server.ru",
            })
            .ToList();

        public IList<Recipient> Recipients { get; set; } = Enumerable.Range(1, 20)
            .Select(i => new Recipient
            {
                Id = i,
                Name = $"Получатель {i}",
                Address = $"recipient_{i}@server.ru",
            })
            .ToList();

        public IList<Server> Servers { get; set; } = Enumerable.Range(1, 5)
            .Select(i => new Server
            {
                Id = i,
                Name = $"Имя {i}",
                Address = $"smtp.server{i}",
                Login = $"Пользователь {i}",
                Password = TextEncoder.Decode($"Пароль{i}"),
                UseSSL = i % 2 == 0
            })
            .ToList();

        public IList<Mail> Mails { get; set; } = Enumerable.Range(1, 20)
            .Select(i => new Mail
            {
                Id = i,
                Subject = $"Заголовок {i}",
                Body = $"Текст сообщения {i}."
            })
            .ToList();
    }
}
