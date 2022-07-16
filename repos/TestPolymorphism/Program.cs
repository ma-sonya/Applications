using System;

namespace TestPolymorphism
{
            class TestOverload
        {
            public void DisplayOverload(int a, string b)
            {
                Console.WriteLine("DisplayOverload int a string b");
            }
        }
    class Program
    {
        static void Main(string[] args)
        {
            TestOverload to = new TestOverload();

            to.DisplayOverload(1,"a");
            Console.ReadKey();

        }
    }
}
