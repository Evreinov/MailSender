using System;
using System.Threading;
using System.Collections.Generic;

namespace ThreadHomeWork
{
    class Task2 : ContextBoundObject
    {
        List<Dictionary<string, string>> _result;
        CsvParser parser = new CsvParser();
        public Task2()
        {
            Start();
        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Задача №2.");

            Console.WriteLine("Написать приложение, выполняющее парсинг CSV-файла произвольной структуры ");
            Console.WriteLine("и сохраняющего его в обычный txt-файл. Все операции проходят в потоках.");
            Console.WriteLine("CSV - файл заведомо имеет большой объем.\n\n");

            Thread thread1 = new Thread(new ThreadStart(ReadWrite));
            thread1.Name = "Парсинг и запись";
            thread1.Start();
            Console.WriteLine($"Ждем окончания работы потока \"{thread1.Name}\".");


            Console.ReadKey();
            Console.Clear();
            Navigation.Show();
        }

        void ReadWrite()
        {
            _result = parser.Parse("file.csv", ',');
            Console.WriteLine("Парсинг данных завершен.");
            Console.WriteLine("Начата запись...");
            parser.Write("test.txt", _result);
            Console.WriteLine("Запись данных завершена.");
            System.Diagnostics.Process.Start("test.txt");
            Console.WriteLine("Нажмите любую клавишу.");
        }
    }
}
