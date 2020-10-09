using System;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHomeWork
{
    class Task1
    {
        private int _n;
        public Task1()
        {
            Start();
        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Задача №1.");
            Console.WriteLine("Написать приложение, считающее в раздельных потоках:");
            Console.WriteLine("a. факториал числа N, которое вводится с клавиатуры;");
            Console.WriteLine("b. сумму целых чисел до N.\n\n");

            Console.WriteLine("Введите положительное целое число.");
            _n = Validator.ParseInNum();

            Thread thread1 = new Thread(new ThreadStart(CalculateFact));
            thread1.Name = "Поток вычисляющий факториал";
            thread1.Start();
            Console.WriteLine($"Ждем окончания работы потока \"{thread1.Name}\".");

            Thread thread2 = new Thread(new ThreadStart(CalculateSum));
            thread2.Name = "Поток вычисляющий сумму";
            thread2.Start();
            Console.WriteLine($"Ждем окончания работы потока \"{thread2.Name}\".");

            Console.ReadKey();
            Console.Clear();
            Navigation.Show();
        }

        void CalculateFact()
        {
            Console.WriteLine($"Факториал числа {_n} равен {Calculator.CalculateFactorial(_n)}.");
        }

        void CalculateSum()
        {
            Console.WriteLine($"Сумма целых чисел до {_n} равна {Calculator.CalculateSumNumbers(_n)}.");
        }


    }
}
