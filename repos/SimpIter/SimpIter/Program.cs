using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpIter
{
    class Program
    {
        static double index = 0, epsilon;

        public static double F(double x)
        {
                return x * x * x - 4 * x * x - 4 * x + 16;
        }

        public static double F1(double x)
        {
            return Math.Sqrt(x * x * x - 4 * x + 16) / 2;
        }

        public static double F1_1(double x)
        {
            return (3 * x * x - 4) / (4 * Math.Sqrt(x * x * x - 4 * x + 16));
        }

        static double Max(double a, double b, Func<double, double> func)
        {
            return Math.Max(Math.Abs(func(a)), Math.Abs(func(b)));
        }
    }
}