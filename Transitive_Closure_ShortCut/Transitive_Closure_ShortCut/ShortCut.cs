using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitive_Closure_ShortCut
{
    class ShortCut
    {
        // поиск кратчайшего пути
        public static int[,] GetShortCut(int[,] array, int length)
        {
            // по алгоритму Флойда петель быть не должно, поэтому вносим 0, потому что
            // кратчайший путь из вершину в эту же вершину 0
            for (int i = 0; i < length; i++)
                array[i, i] = 0;
            // сравниваем значения дуг у смежных вершин, ищем минимальное значение там, где есть направляющая дуга
            for (int k = 0; k < length; k++)
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                        if(array[i, k] < int.MaxValue && array[k, j] < int.MaxValue)// проверяем на существование дуги
                            if (array[i, k] + array[k, j] < array[i, j])
                                array[i, j] = array[i, k] + array[k, j];
            // возвращаем массив
            return array;
        }
    }
}
