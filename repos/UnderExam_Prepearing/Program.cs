using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderExam_Prepearing
{

    struct A
    {
        public string str;
        public B b;
    }

    class B
    {
        public string str;
        public A a;
    }

    class Program
    {
        static void Main(string[] args)
        {
            A s1 = new A();
            s1.str = "1";

            B c1 = new B();
            c1.str = "3";
            c1.a = s1;
            c1.a.str = "4";


            s1.b = c1;
            s1.b.str = "2";

            A s2 = s1;
            s2.str = "5";
            s2.b.str = "6";


            B c2 = c1;
            c2.str = "7";
            c2.a.str = "8";

            Console.WriteLine(s1.str + s1.b.str + s2.str + s2.b.str);
            Console.WriteLine(c1.str + c1.a.str + c2.str + c2.a.str);
            Console.ReadKey();
        }
    }
}
