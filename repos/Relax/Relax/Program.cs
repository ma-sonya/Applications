using System;
using System.IO;

namespace MRelax
{
    class Program
    {
        public static double epsilon=0.001;
        public static double index = 0;

        public static double F(double x)
        {
            return x * x * x + 3 * x * x - x - 3;
        }

        public static double F_1(double x)
        {
            return 3 * x * x + 6 * x - 1;
        }

        public static double F_2(double x)
        {
            return 6 * x + 6;
        }

        static int Main()
        {
            double a=0, b=2;

            Console.Write("Введiть значення епсiлона= ");
            epsilon = Convert.ToDouble(Console.ReadLine());

            if (F_1(a) * F_1(b) > 0)
            {
                Console.WriteLine("Корiнь не належить даному промiжку!");
                return 0;
            }
            else
            {
                if (F_2(a) * F_2(b) < 0)
                {
                    Console.WriteLine("Функцiя не монотонна на даному промiжку!");
                    return 0;
                }
                else
                {
                    double x = b;
                    double x0 = a;
                    double X;
                    if (a >= 0)
                    {
                        X = 1;
                    }
                    else
                        X = -3;

                    double M1 = Math.Max(Math.Abs(F_1(a)), Math.Abs(F_1(b)));
                    double m1 = Math.Min(Math.Abs(F_1(a)), Math.Abs(F_1(b)));
                    double tau = 2 / (M1 + m1);
                    double q = (M1 - m1) / (M1 + m1);
                    double z1 = x - X;
                    double z0 = Math.Abs(z1) / q;

                    double N = Math.Truncate(Math.Log(Math.Abs(z0) / epsilon) / Math.Log(1 / q)) + 1;

                    do
                    {
                        x0 = x;
                        x = Math.Round(x0 - tau * F(x0), 3);

                        Console.WriteLine($"X0 = {x0}");
                        Console.WriteLine($"X = {x}");
                        Console.WriteLine();
                        index++;
                    }
                    while (Math.Abs(x0 - x) > epsilon);

                    Console.WriteLine($"Апостерiорна оцiнка: {N}");
                }
            }

            Console.WriteLine($"Апрiорна оцiнка:     {index}");
            return 143;
        }
    }
}

