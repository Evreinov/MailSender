using MailSender.lib.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MailSender.lib.Services
{
    public class Rfc2898Encryptor : IEncryptorService
    {
        /// <summary>
        /// Массив байт - "соль" алгоритма шифрования Rfc2898
        /// </summary>
        private static readonly byte[] SALT =
        {
            0x26, 0xdc, 0xff, 0x00,
            0xad, 0xed, 0xfa, 0x34,
            0xaa, 0x14, 0xac, 0x45,
            0xba, 0x12, 0x01, 0x12
        };

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>Получить алгоритм шифрования с указанным паролем.</summary>
        /// <param name="password">Пароль шифрования.</param>
        /// <returns>Алгоритм шифрования.</returns>
        private static ICryptoTransform GetAlgorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, SALT);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateEncryptor();
        }

        /// <summary>Получить алгоритм шифрования для расшифровки.</summary>
        /// <param name="password">Пароль шифрования.</param>
        /// <returns>Алгоритм расшифровки.</returns>
        private static ICryptoTransform GetInverseAlgorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, SALT);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateDecryptor();
        }

        public string Decrypt(string str, string password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var crypted_bytes = Convert.FromBase64String(str);
            var bytes = Decrypt(crypted_bytes, password);
            return encoding.GetString(bytes);
        }

        public byte[] Decrypt(byte[] data, string password)
        {
            var algoritm = GetInverseAlgorithm(password);
            using (var stream = new MemoryStream())
            {
                using (var crypto_stream = new CryptoStream(stream, algoritm, CryptoStreamMode.Write))
                {
                    crypto_stream.Write(data, 0, data.Length);
                    crypto_stream.FlushFinalBlock();
                    return stream.ToArray();
                }
            }
        }

        public string Encrypt(string str, string password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            var crypted_bytes = Encrypt(bytes, password);
            return Convert.ToBase64String(crypted_bytes);
        }

        public byte[] Encrypt(byte[] data, string password)
        {
            var algoritm = GetAlgorithm(password);
            using (var stream = new MemoryStream())
            {
                using(var crypto_stream = new CryptoStream(stream, algoritm, CryptoStreamMode.Write))
                {
                    crypto_stream.Write(data, 0, data.Length);
                    crypto_stream.FlushFinalBlock();
                    return stream.ToArray();
                }
            }
        }
    }
}
