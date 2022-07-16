using System;
using System.IO;

namespace MNewton
{
    class Program
    {
        const double epsilon = 1e-3;
        static int index = 0;

        static double F(double x)
        {
            return x * x * x + 4 * x * x - 4 * x - 4;
        }
        static double F1(double x)
        {
            return 3 * x * x + 8 * x - 4;
        }
        static double F2(double x)
        {
            return 6 * x + 8;
        }
        static double NewtonsM(double a, double b)
        {
            double x0, x1, x2;            

            if ((F(a) * F(b) < 0))
            {
                if ((F1(b) > 0 && F2(b) > 0) || (F1(b) < 0 && F2(b) < 0))
                {
                    x0 = b;
                }
                else
                    x0 = a;

                if ((F(x0) * F2(x0) > 0))
                {
                    double x = 1.321;

                    double MIN = Math.Min(Math.Abs(F1(a)), Math.Abs(F1(b)));
                    double MAX = Math.Max(Math.Abs(F2(a)), Math.Abs(F2(b)));
                    double q = (MAX * Math.Abs(x0 - x)) / (2 * MIN);

                    if (q<1)
                    {
                        double N = (Math.Truncate(Math.Log2((Math.Log((Math.Abs(x0 - x)) / epsilon)) / (Math.Log(1 / q))) + 1) + 1);
                        x2 = x0;

                        do
                        {
                            if (index >= 1000)
                            {
                                break;
                            }

                            x1 = x2;
                            x2 = x1 - (F(x1) / F1(x1));

                            Console.WriteLine("Iтерацiя:      {0}", index);
                            Console.WriteLine("Лiва границя:  {0}", x1);
                            Console.WriteLine("Права границя: {0}", x2);
                            Console.WriteLine();

                            index++;
                        }
                        while (Math.Abs(x2 - x1) > epsilon);

                        Console.WriteLine("Апостерiорна оцiнка: {0}", N);
                        return x2;
                    }
                    else
                    {
                        Console.WriteLine("Не виконуються умови теореми про збiжнiсть!");
                        Console.WriteLine("q = {0}", q);
                        return 0;
                    }                    
                }
                else
                {
                    Console.WriteLine("Не виконуються умови 1 теореми: Початкове наближення не задовольняє умову!");
                    Console.WriteLine("F(x0) * F2(x0) = {0}", F(x0) * F2(x0));
                    return 0;
                }                
            }
            else
            {
                Console.WriteLine("Не виконуються умови 1 теореми: Функцiя на цих межах приймає однаковий знак!");
                Console.WriteLine("F(a) * F(b) = {0}", F(a) * F(b));
                return 0;
            }                      
        }
        static void Main(string[] args)
        {
            double a, b;

            Console.Write("Введiть лiву границю : ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введiть праву границю: ");
            b = Convert.ToDouble(Console.ReadLine());

            double x2 = NewtonsM(a, b);

            x2 = Math.Round(x2, 4);

            if (index >= 1000)
                Console.WriteLine("Немає кореня");
            else if (x2 >= a && x2 <= b)
            {
                Console.WriteLine($"\nКорiнь рiвняння = {x2}");
            }            
        }
    }
}
