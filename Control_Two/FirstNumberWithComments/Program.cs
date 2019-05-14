using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ControlTask_1
{
    // класс данных: имя, свойство, то есть значение, и метод
    class MyClass
    {
        public string Name { get; set; }
        public int Property { get; set; }
        public int Method(int argument)
        {
            return Property + argument;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // путь до папки
            string folder = @"C:\Users\Дмитрий\source\repos\ControlTask_1\ControlTask_1\folder";
            // выводим все каталоги и папки
            GetInformationAboutFilesAndDirectory(folder);
            Console.WriteLine("\n");
            // получаем результат
            var variable = GetRightAnswer();
            // считывает папки
            var newFolders = Directory.GetDirectories(folder);
            // количество правильных ответов
            int count = 0;
            foreach (var e in newFolders)
            {
                // берем файлы
                var newFiles = Directory.GetFiles(e);
                foreach (var s in newFiles)
                {
                    // считываем ответ и сравниваем с ответом в нашем классе
                    var str = File.ReadAllLines(s);
                    var answer = int.Parse(str[0]);
                    if (variable == answer)
                        count++;// увеличиваем счетчик
                }
                var information = new MyClass { Name = e, Property = count };
                // заносим результат в таблицу, где отображается имя и количество верных ответов
                Print(information);
                // обнуляем счетчик
                count = 0;
            }
            Console.ReadKey();
        }
        // заполняем тадлицу
        static void Print(object obj)
        {

            var builder = new StringBuilder();
            // заводим тип
            var type = obj.GetType();
            foreach (var property in type.GetProperties())
            {
                builder.Append(property.Name);
                builder.Append(" : ");
                builder.Append(property.GetValue(obj));
                builder.Append("\n");
            }
            // выводим данные
            var str = builder.ToString();
            Console.WriteLine(str);
        }
        // вычисляем ответ
        static int GetRightAnswer()
        {
            var type = typeof(MyClass);
            ConstructorInfo ctor = type.GetConstructor(new Type[] { });
            var result = ctor.Invoke(new object[] { });
            // присваиваем значения
            var propertyInfo = type.GetProperty("Property");
            propertyInfo.SetValue(result, 5);
            // считаем значение в методе
            var argument = int.Parse(Console.ReadLine());
            return (int)type.GetMethod("Method").Invoke(result, new object[] { argument });
        }
        // метод, отвечающий за вывод всех каталогов и файлов
        static void GetInformationAboutFilesAndDirectory(string folder)
        {
            if (Directory.Exists(folder))
            {
                // выводим каталоги - названия папок
                Console.WriteLine("Folders:");
                var folders = Directory.GetDirectories(folder);
                foreach (var e in folders)
                    Console.WriteLine(e);
                //выводим файлы, содержащиеся в каталогах
                Console.WriteLine("Files:");
                foreach (var e in folders)
                {
                    var newFiles = Directory.GetFiles(e);
                    foreach (var s in newFiles)
                        Console.WriteLine(s);
                }
            }
        }
    }
}
