using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace contwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var colors = new List<string> { "green", "brown", "blue", "red" };
            var querty = colors.Where(c => c.Length == 5);
            Console.WriteLine(querty.Count() + " ");
            colors.Remove("green");
            Console.WriteLine(querty.Count() + " ");
            colors.Add("black");
            Console.WriteLine(querty.Count() + " ");
        }
    }
}
