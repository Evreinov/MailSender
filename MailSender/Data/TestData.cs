using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MailSender.Models;
using System.Xaml;
using MailSender.lib.Services;

namespace MailSender.Data
{
    static class TestData
    {
        public static List<Sender> Senders { get; } = Enumerable.Range(1, 20)
            .Select(i => new Sender
            {
                Id = i,
                Name = $"Отправитель {i}",
                Address = $"sender_{i}@server.ru",
            })
            .ToList();

        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 20)
            .Select(i => new Recipient
            {
                Id = i,
                Name = $"Получатель {i}",
                Address = $"recipient_{i}@server.ru",
            })
            .ToList();

        public static List<Server> Servers { get; } = Enumerable.Range(1, 5)
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

        public static List<Mail> Messages { get; } = Enumerable.Range(1, 20)
            .Select(i => new Mail
            {
                Id = i,
                Subject = $"Заголовок {i}",
                Body = $"Текст сообщения {i}."
            })
            .ToList();
    }
}
