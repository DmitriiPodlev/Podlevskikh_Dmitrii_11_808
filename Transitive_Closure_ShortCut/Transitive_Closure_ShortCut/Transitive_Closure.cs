using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitive_Closure_ShortCut
{
    class Transitive_Closure_ShortCut
    {
       // построение транзитивного замыкания
       public static int[,] GetTransitiveClosure(int[,] array, int length)
        {
            // тройным циклом обходим дуги, строим новые на основе алгоритма Уоршелла,
            // если выполняется условие транзитивного замыкания, проводим дугу
            for (int k = 0; k < length; k++)
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                        if (array[i, j] == 0)
                            array[i, j] = array[i, k] * array[k, j];
            // возвращаем матрицу
            return array;
        }
    }
}
