using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ControlTask_1
{
    class MyClass
    {
        public string Name;
        public int Property { get; set; }
        public int Method(int argument)
        {
            return Property;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string folder = @"C:\Users\Дмитрий\source\repos\ControlTask_1\ControlTask_1\folder"; 
            if (Directory.Exists(folder))
            {
                Console.WriteLine("Folders:");
                var folders = Directory.GetDirectories(folder);
                foreach (var e in folders)
                    Console.WriteLine(e);
                Console.WriteLine("Files:");
                foreach (var e in folders)
                {
                    var newFiles = Directory.GetFiles(e);
                    foreach (var s in newFiles)
                        Console.WriteLine(s);
                }
            }
            Console.WriteLine("\n");
            var type = typeof(MyClass);

            ConstructorInfo ctor = type.GetConstructor(new Type[] { });
            var result = ctor.Invoke(new object[] { });

            var propertyInfo = type.GetProperty("Property");
            propertyInfo.SetValue(result, 5);
            var variable1 = (int)type.GetMethod("Method").Invoke(result, new object[] { 3 });

            var newFolders = Directory.GetDirectories(folder);
            foreach (var e in newFolders)
            {
                var newFiles = Directory.GetFiles(e);
                foreach (var s in newFiles)
                {
                    var str = File.ReadAllLines(s);
                    var answer = int.Parse(str[0]);
                    if (variable1 == answer)
                    {
                        var information = new MyClass { Name = e, Property = answer };
                        Print(information);
                    }

                }
            }
        }

        static void Print(object obj)
        {
            var builder = new StringBuilder();

            var type = obj.GetType();
            foreach (var property in type.GetProperties())
            {
                builder.Append(property.Name);
                builder.Append(" : ");
                builder.Append(property.GetValue(obj));
                builder.Append("\n");
            }
            var str = builder.ToString();
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
