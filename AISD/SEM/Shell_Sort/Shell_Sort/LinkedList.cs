using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell_Sort
{
    public class LinkedList
    {
        // считаем итерации
        public static int Iterations;
        // сортировка Шелла
        public static int[] ShellSortOnLinkedList(int[] array)
        {
            // заводим двухсвязный список, добавляем элементы массива
            var linkedList = new LinkedList<int>();
            for (int i = 0; i < array.Length; i++)
                linkedList.AddLast(array[i]);
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
                    // две новые переменные, отвечающие за ссылки на следующий узел linkedList
                    var node1 = linkedList.First;
                    var node2 = linkedList.First;
                    // делаем ссылку на узел с порядком, соответствующим j
                    for (int count = 0; count < j; count++)
                        node1 = node1.Next;
                    node2 = node1;
                    //  делаем ссылку на узел с порядком, соответствующим j + step - шаг
                    for (int count = j; count < step; count++)
                        node2 = node2.Next;
                    /*сравниваем значения элементов с определенным шагом, затем уменьшаем шаг, 
                    чтобы в случае чего поменять местами значения предыдущего и следующего от исходного элемента*/
                    while ((j >= 0) && (node1.Value > node2.Value))
                    {
                        // меняем местами элементы
                        int tmp = node1.Value;
                        node1.Value = node2.Value;
                        node2.Value = tmp;
                        j -= step;
                        node2 = node1;
                        // переводим узел на позицию узла с j - step, увеличиваем число итераций
                        for (int count = j; count > j - step; count--)
                        {
                            node1 = node1.Previous;
                            Iterations++;
                        }
                    }
                }
                // уменьшаем шаг в 2 раза
                step = step / 2;
            }
            // заводим массив, в который добавляем значения элементов в linkedList
            var answerArray = new int[array.Length];
            var node = linkedList.First;
            for (int i = 0; i < array.Length; i++)
            {
                answerArray[i] = node.Value;
                node = node.Next;
            }
            // возвращаем массив
            return answerArray;
        }
    }
}
