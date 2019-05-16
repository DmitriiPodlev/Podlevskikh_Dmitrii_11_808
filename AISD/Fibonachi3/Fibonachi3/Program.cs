using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi3
{
    public class Matrix
    {
        public int a;
        public int b;
        public int c;
        public int d;
    }

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var matrix = new Matrix { a = 1, b = 1, c = 1, d = 2 };
            var newMatrix = GetMatrix(matrix, n);
            var answerMatrix = new Matrix { a = newMatrix.a * 0 + newMatrix.b * 1, b = newMatrix.c * 0 + newMatrix.d * 1 };
            Console.WriteLine(answerMatrix.a);
            Console.ReadKey();
        }

        static Matrix GetMatrix(Matrix matrix, int n)
        {
            Matrix newMatrix;
            if (n % 2 == 0)
                newMatrix = new Matrix { a = 1, b = 0, c = 0, d = 1 };
            else
                newMatrix = new Matrix { a = 0, b = 1, c = 1, d = 1 };
            for (int i = 0; i < n / 2; i++)
            {
                newMatrix = new Matrix
                {
                    a = newMatrix.a * matrix.a + newMatrix.b * matrix.c,
                    b = newMatrix.a * matrix.b + newMatrix.b * matrix.d,
                    c = newMatrix.c * matrix.a + newMatrix.d * matrix.c,
                    d = newMatrix.c * matrix.b + newMatrix.d * matrix.d
                };
            }
            return newMatrix;
        }
    }
}
