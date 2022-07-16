using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson1
{
    class Class1
    {
        public int number;
        public Class1(int number)
        {
            this.number = number;
            Console.WriteLine("Hi, I was creaated!");
        }
        public void Say()
        {
            Console.WriteLine("{0} is my number!", this.number);
        }
    }
}
