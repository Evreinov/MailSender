using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadHomeWork
{
    static class Validator
    {
        public static int ParseInNum()
        {
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Введите число.");
            };
            return n;
        }
    }
}
