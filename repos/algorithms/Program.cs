using System;
using System.Collections.Generic;

namespace algorithms
{
    class Program
    {
        static int maximum(List<int> list)
        {
            int mmax = list[0];
            if(list.Count > 0)
            {
                List<int> new_list = new List<int> ();
                new_list = list.GetRange(0, list.Count);
                new_list.RemoveAt(0);
                if (new_list.Count > 0)
                {
                    int new_mmax = maximum(new_list);
                    if (new_mmax > mmax)
                    {
                        mmax = new_mmax;
                    }
                }
                }
                return mmax;
        }
       
        static void Main(string[] args)
        {
            List<int> s = new List<int>() { 1, 5, 3, 111,4 , 57, 41, 11 };
            Console.WriteLine(maximum(s));
        }
    }
}
