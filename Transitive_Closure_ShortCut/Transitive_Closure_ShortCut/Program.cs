using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitive_Closure_ShortCut
{
    class Program
    {
        static void Main(string[] args)
        {
            // считываем данные
            var data = File.ReadAllLines(@"C:\Users\Дмитрий\source\repos\Transitive_Closure_ShortCut\Transitive_Closure_ShortCut\Data.txt");
            // генерируем их, вносим в матрицу
            var array = Generator.GenerateData(data);
            // ищем кратчайший путь
            ShortCut.GetShortCut(array, data.Length);
            // выводим в файле ответ в виде матрицы
            Print.WriteAnswer(array, data.Length);
        }
    }
}
