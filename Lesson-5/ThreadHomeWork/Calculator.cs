using System.Linq;
using System.Numerics;

namespace ThreadHomeWork
{
    static class Calculator
    {
        /// <summary>
        /// Возвращает факториал числа.
        /// </summary>
        /// <param name="n">Целое число.</param>
        /// <returns>Факториал числа.</returns>
        public static BigInteger CalculateFactorial(int n)
        {
            if (n == 0) 
                return 1;
            return Enumerable.Range(1, n).Aggregate(new BigInteger(1),(f, i) => f * i);
        }
        /// <summary>
        /// Возвращает сумму целых чисел до n.
        /// </summary>
        /// <param name="n">Целое число.</param>
        /// <returns>Сумма целых чисел до n.</returns>
        public static int CalculateSumNumbers(int n)
        {
            return Enumerable.Range(1, n - 1).Aggregate((f, i) => f + i);
        }
    }
}
