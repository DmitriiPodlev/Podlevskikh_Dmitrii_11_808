using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibonachi1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int answer = GetAnswer(n);
            Console.WriteLine(answer);
            Console.ReadKey();
        }
        static int GetAnswer(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            return GetAnswer(n - 2) + GetAnswer(n - 1);
        }
    }
}
