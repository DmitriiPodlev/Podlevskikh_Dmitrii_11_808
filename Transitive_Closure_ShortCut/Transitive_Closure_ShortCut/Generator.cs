using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitive_Closure_ShortCut
{
    class Generator
    {
        // генерируем данные
        public static int[,] GenerateData(string[] data)
        {
            // искомый двумерный массив
            var array = new int[data.Length, data.Length];
            // строка для элементов строк матрицы
            string[] str;
            for (int j = 0; j < data.Length; j++)
            {
                str = data[j].Split();
                for (int i = 0; i < str.Length; i++)
                {
                    // парсим в int
                    array[i, j] = Int32.Parse(str[i]);
                    // если значение в ячейке 0, то по алгоритму Флойда пишем значение максимального элемента
                    if (array[i, j] == 0)
                        array[i, j] = int.MaxValue;
                }
            }
            // возвращаем матрицу
            return array;
        }
    }
}
