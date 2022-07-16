using System;

namespace ClassesTest1
{
    class ClassA: ClassB
    {
        public void Method1()
        {
            System.Console.WriteLine("ClassA Method1");
           // base.Method1();
        }
    }

    class ClassB
    {
        public string str = "aaa";
        public void Method1()
        {
            Console.WriteLine("ClassB Method1");
        }
        public void Method2()
        {
            Console.WriteLine("ClassB Method2");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ClassB classB = new ClassB();
            ClassA classA = new ClassA();
         //   classB = classA;
         //   classA = (ClassA)classB;

            classA.Method1();
            Console.ReadKey();
        }
    }
}
