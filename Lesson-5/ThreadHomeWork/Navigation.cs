using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHomeWork
{
    static class Navigation
    {
        public static void Show()
        {
            Console.WriteLine("Домашнее задание к уроку №5 многопоточное программирование.");
            Console.WriteLine("1. Приложение, считающее в раздельных потоках.\n2. Парсинг CSV.\n0. Выход.\n");
            Console.WriteLine("Выберите пункт меню.");
            int n = -1;
            while (n < 0 || n > 2)
            {
                n = Validator.ParseInNum();
                Console.WriteLine("Число должно совпадать с пунктом меню.");
            };

            switch (n)
            {
                case 1:
                    new Task1();
                    break;
                case 2:
                    new Task2();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
