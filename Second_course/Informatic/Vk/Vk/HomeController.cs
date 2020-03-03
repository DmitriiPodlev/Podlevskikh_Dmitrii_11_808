using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vk.Services;

namespace Vk
{
    public class HomeController
    {
        private IStorage storage;

        public HomeController(IStorage storage)
        {
            this.storage = storage;
        }

        public async Task GetForm(HttpContext context)
        {
            await context.Response.WriteAsync(File.ReadAllText("Views\\Post.html"));
        }
        // сохранение
        public async Task AddEntry(HttpContext context)
        {
            var filePath = "Files";

            var fileCount = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories).Length;
            fileCount++;

            foreach (var formFile in context.Request.Form.Files)
            {
                if (formFile.Length > 0)
                {
                    string newFile = Path.Combine(filePath, fileCount + Path.GetExtension(formFile.FileName));
                    using (var inputStream = new FileStream(newFile, FileMode.Create))
                    {
                        // read file to stream
                        await formFile.CopyToAsync(inputStream);
                        // stream to byte array
                        byte[] array = new byte[inputStream.Length];
                        inputStream.Seek(0, SeekOrigin.Begin);
                        inputStream.Read(array, 0, array.Length);
                        // get file name
                        string fName = formFile.FileName;
                    }
                }
            }

            var name = context.Request.Form["name"];
            var text = context.Request.Form["text"];
            var date = DateTime.Now.ToShortDateString();
            var txtFileName = Path.Combine(filePath, fileCount + ".txt");
            File.AppendAllLines(txtFileName, new string[] { name, text, date});

            await context.Response.WriteAsync("New Entry was added");
        }
        // получение постов
        public async Task GetPosts(HttpContext context)
        {
            string filePath = "Files";
            string[] files = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories);
            var page = new StringBuilder();
            page.Append(@"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset=""utf-8"" />
                    <title>Все посты:</title>
                </head>
                <body>
            ");
            // проходимся по файлам
            foreach (var file in files)
            {
                var id = file.Substring(6);
                id = id.Substring(0, id.Length - 4);
                var data = File.ReadAllLines(file);
                page.Append(String.Format(@" <div class = ""post"">
                    <h3><img src=""{1}"" ></h3>
                    <h4>{2}</h4>
                    <h5>{3}</h5>
                    <h5>{4}</h5>
                    <form action=""/Home/EditPost/{0}"" method=""get"" enctype=""multipart / form - data"" >
                        <input type=""submit"" value=""Редактировать""/>
                    </form>
                    <form action=""/Home/RemovePost/{0}"" method=""post"" enctype=""multipart / form - data"" >
                        <input type=""submit"" value=""Удалить""/>
                    </form>
                </div>", id, $"Files/{id}.png", data[0], data[1], data[2]));
            }
            // добавив данные, завершаем html-код
            page.Append(@"</body>
            </html>");
            await context.Response.WriteAsync(page.ToString());
        }
        // удаление поста
        public async Task RemovePost(HttpContext context)
        {
            var id = context.Request.Path.Value.Split('/').Last();
            File.Delete($"Files/{id}.txt");
            File.Delete($"Files/{id}.png");
            await GetPosts(context);
        }
        // редактирование поста
        public async Task EditPost(HttpContext context)
        {
            var id = context.Request.Path.Value.Split('/').Last();
            if (context.Request.Method == "GET")
            {
                var data = File.ReadAllLines($"Files/{id}.txt");
                var page = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta charset=""utf-8"" />
                        <title>Редактирование:</title>
                    </head>
                    <body>
                        <form action=""/Home/EditPost/{0}"" method=""post"" enctype=""multipart / form - data"" >
                            <input name=""name"" value=""{1}"" /><br /><br />
                            <textarea name=""text"">{2}</textarea><br /><br />
                            <input type=""submit"" value=""Отредактировать""/>
                        </form>
                    </body>
                </html>", id, data[0], data[1]);
                await context.Response.WriteAsync(page);
            }
            else
            {
                var name = context.Request.Form["name"];
                var text = context.Request.Form["text"];
                var date = DateTime.Now.ToShortDateString();
                var txtFileName = Path.Combine("Files", id + ".txt");
                File.WriteAllLines(txtFileName, new string[] { name, text, date });
                await GetPosts(context);
            }
        }
    }
}