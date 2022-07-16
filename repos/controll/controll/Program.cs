using System;

namespace HelloWorld
{
    public class A
    {
        public void M1()
        {
            Console.Write("1");
        }
        public virtual void M2()
        {
            Console.Write("2");
        }
    }

    public class B: A
    {
        public new void M1()
        {
            Console.Write("3");
        }
        public override void M2()
        {
            Console.Write("4");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            A p1 = new A();
            B p2 = new B();
            A p3 = new B();
            p1.M1(); p1.M2();
            p2.M1(); p2.M2();
            p3.M1(); p3.M2();
        }
    }
}