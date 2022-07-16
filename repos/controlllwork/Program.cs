using System;

namespace controlllwork
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10, b = 0;
            try
            {
                var c = a / b;
                Console.Write("1");
            }
            catch(DivideByZeroException) when (a>5 && b < 5)
            {
                Console.Write("2");
            }
            catch
            {
                Console.Write("3");
            }
            finally
            {
                Console.Write("4");
            }
            Console.WriteLine("5");
        }
    }
}
