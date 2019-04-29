using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitive_Closure_ShortCut
{
    class Print
    {
        // выводим ответ в файл
        public static void WriteAnswer(int[,] array, int length)
        {
            // переменная для записи строки матрицы
            string arrayString = null;
            // искомый ответ
            var answer = new List<string>();
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    // если в ячейке максимальное значение, то выводим 0, потому что дуги нет
                    if (array[i, j] == int.MaxValue)
                        array[i, j] = 0;
                    // записываем строку
                    arrayString = arrayString + array[i, j].ToString() + " ";
                }
                // добавляем строку
                answer.Add(arrayString);
                arrayString = null;
            }
            // заносим в файл
            File.WriteAllLines(@"C:\Users\Дмитрий\source\repos\Transitive_Closure_ShortCut\Transitive_Closure_ShortCut\Answer.txt", answer);
            answer.Clear();
        }
    }
}
