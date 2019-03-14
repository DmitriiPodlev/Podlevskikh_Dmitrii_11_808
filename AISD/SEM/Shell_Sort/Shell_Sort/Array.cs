using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell_Sort
{
    class Array
    {
        // считаем итерации
        public static int Iterations;
        // сортировка Шелла
        public static int[] ShellSortOnArray(int[] array)
        {
            int j;
            // начальный шаг
            int step = array.Length / 2;
            // выполняется, пока шаг больше 0
            while (step > 0)
            {
                // проходим по элементам, пока i < array.Length - step, чтобы сравнить каждый элемент с i + step
                for (int i = 0; i < (array.Length - step); i++)
                {
                    j = i;
                    /*сравниваем значения элементов с определенным шагом, затем уменьшаем шаг, 
                    чтобы в случае чего поменять местами значения предыдущего и следующего от исходного элемента*/
                    while ((j >= 0) && (array[j] > array[j + step]))
                    {
                        // меняем местами элементы, увеличиваем число итераций
                        int tmp = array[j];
                        array[j] = array[j + step];
                        array[j + step] = tmp;
                        j -= step;
                        Iterations++;
                    }
                }
                // уменьшаем шаг в 2 раза
                step = step / 2;
            }
            // возвращаем наш остортированный массив
            return array;
        }
    }
}
