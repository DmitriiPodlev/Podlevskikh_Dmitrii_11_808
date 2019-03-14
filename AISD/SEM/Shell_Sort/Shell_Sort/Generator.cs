using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell_Sort
{
    class Generator
    {
        // генерируем данные
        public static void GenerateNumbers(int length, int numberOfFile)
        {
            var n = new Random();
            // заводим строку для добавления символов из рандома
            var str = new List<string>();
            // добавляем элементы и вписываем в файл
            for (int i = 0; i < length; i++)
                str.Add(n.Next().ToString());
            File.WriteAllLines(String.Format("data{0}.txt", numberOfFile), str);
            str.Clear();
        }
    }
}
