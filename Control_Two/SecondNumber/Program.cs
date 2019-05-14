using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlTask_2
{
    // необходимые поля, класс нужной информации
    class Information
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new StringBuilder();
            int sum = 0;// сумма букв каждой строки
            var files = new List<string>();
            foreach (var json in files)
            {
                // десериализируем каждую строку списка в объект 
                Information information = JsonConvert.DeserializeObject<Information>(json);
                if (information.id % 2 == 0)// номер должен быть четным
                    foreach (var e in information.body)
                        if (e >= 'a' && e <= 'z')
                            sum++;// считаем только буквы
                // печатаем для каждой строки ее сумму
                builder.Append(information.name);
                builder.Append(" : ");
                builder.Append(sum);
                builder.Append("\n");
                // обнуляем, идем в другую строку
                sum = 0;
            }
            var str = builder.ToString();
            Console.WriteLine(str);
        }
    }
}
