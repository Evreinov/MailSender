using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MailSender.lib.Interface;
using MailSender.lib.Models;

namespace MailSender.lib.Services
{
    public class InXmlFileDataStorage : IServersStorage, ISendersStorage, IRecipientsStorage, IMailsStorage
    {
        public class DataStructure
        {
            public ICollection<Server> Servers { get; set; } = new List<Server>();
            public ICollection<Sender> Senders { get; set; } = new List<Sender>();
            public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
            public ICollection<Mail> Mails { get; set; } = new List<Mail>();
        }

        private readonly string _FileName;

        public InXmlFileDataStorage(string FileName) => _FileName = FileName;

        private DataStructure Data { get; set; } = new DataStructure();

        ICollection<Server> IStorage<Server>.Items => Data.Servers;
        ICollection<Sender> IStorage<Sender>.Items => Data.Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Data.Recipients;
        ICollection<Mail> IStorage<Mail>.Items => Data.Mails;

        public object GlobalConfiguration { get; private set; }

        public void Load()
        {
            if (!File.Exists(_FileName)) 
            { 
                Data = new DataStructure(); 
                return; 
            }

            using (var file = File.OpenText(_FileName))
            {
                if (file.BaseStream.Length == 0)
                {
                    Data = new DataStructure();
                    return;
                }

                var serializer = new XmlSerializer(typeof(DataStructure));
                Data = (DataStructure)serializer.Deserialize(file);
            } 
        }

        public void SaveChanges()
        {
            using (var file = File.CreateText(_FileName))
            {
                var serializer = new XmlSerializer(typeof(DataStructure));
                serializer.Serialize(file, Data);
            } 
        }
    }
}
