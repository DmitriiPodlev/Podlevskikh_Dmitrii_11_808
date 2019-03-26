using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_second
{
    class Program
    {
        private static IEnumerable<Tuple<T, T>> GetTuples<T>( IEnumerable<T> item1, IEnumerable<T> item2, Func<T, T, bool> filter)
        {
            foreach (var e in item1)
                foreach (var t in item2)
                {
                    if (filter(e, t))
                        yield return Tuple.Create(e, t);
                }
        }
    }
}
