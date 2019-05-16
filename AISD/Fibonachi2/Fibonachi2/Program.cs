using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi2
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
            int a = 0;
            int b = 1;
            int c = 0;
            for (int i = 2; i <= n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }
            return c;
        }
    }
}
