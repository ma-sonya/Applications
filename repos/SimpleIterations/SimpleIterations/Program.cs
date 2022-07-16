using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIterations
{
    class Program
    {
        static double index = 0, epsilon = 1e-3;

        public static double F(double x)
        {
            return x * x * x - 4 * x * x - 4 * x + 16;
        }

        public static double F1(double x)
        {
            return Math.Sqrt((x * x * x) / 4 - x + 4);
        }

        public static double F1_1(double x)
        {
            return ((3 * x * x) / 4 - 1) / (2 * Math.Sqrt((x * x * x) / 4 - x + 4));
        }

        //а - початок проміжку, b-кінець проміжку 
        //func - підставиться функція φ'(x)
        static double Max(double a, double b, Func<double, double> func)
        {
            return Math.Max(Math.Abs(func(a)), Math.Abs(func(b)));
        }

        static double MSI(double q, double x0, Func<double, double> func)
        {
            double umova, x1;

            if (q < 0.5)
            {
                umova = epsilon * (1 - q) / q;
            }
            else
                umova = epsilon;

            x1 = x0;

            do
            {
                x0 = Math.Round(x1, 2);
                x1 = Math.Round(func(x0), 2);
                index++;
                Console.WriteLine($"Iтерацiя {index}   x = {x0}");
            }
            while (Math.Abs(x1 - x0) > umova);

            return x1;
        }

        static int Main()
        {
            double a=1.5, b=2.2;

            Console.Write("Введiть значення епсiлона = ");
            epsilon = Convert.ToDouble(Console.ReadLine());

            double d = Math.Abs(b - a);

            double q;

            double result = 0;
            double _N = 0;

            q = Max(a, b, F1_1);
            if (q < 1)
            {
                var c = (a + b) / 2;
                _N = Math.Truncate(Math.Log(Math.Abs(F1(c) - c) / ((1 - q) * epsilon)) / (Math.Log(1 / q))) + 1;
                result = MSI(q, c, F1);
            }

            if (result != 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Корiнь рiвний        х = {result}");
                Console.WriteLine($"Апрiорна оцiнка      N = {index}");
                Console.WriteLine($"Апостерiорна оцiнка _N = {_N}");
            }
            else
                Console.WriteLine("Не вдалося знайти розв'язок. Погано обранi межi");

            return 0;
        }
    }
}