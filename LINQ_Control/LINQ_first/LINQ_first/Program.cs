using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_first
{
    class Program
    {
        static void GetAnswer(List<int> list)
        {
            int i = 0;
            var newList = list
                .Select(x =>
            {
                i++;
                return x * i;                
            })
            .Where(x => x >= 10 && x <= 99)
            .Reverse();
            foreach (var e in newList)
                Console.Write(e + " ");
        }


        static void Main(string[] args)
        {
            var list = new List<int> { 11, 12, 13, 345 };
            GetAnswer(list);
            Console.ReadKey();
        }
    }
}
