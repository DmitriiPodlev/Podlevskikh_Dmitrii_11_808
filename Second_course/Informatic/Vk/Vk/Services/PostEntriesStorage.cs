using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vk.Models;

namespace Vk.Services
{
    public class PostEntriesStorage : IStorage
    {
        // загрузка данных
        public List<Dictionary<string, string>> Load(HttpContext context)
        {
            string filePath = "Files";
            string[] files = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories);
            var loader = new List<Dictionary<string, string>>();
            // заносим данные в loader
            foreach (var file in files)
            {
                var id = file.Substring(6);
                id = id.Substring(0, id.Length - 4);
                var data = File.ReadAllLines(file);
                loader.Add(new Dictionary<string, string>
                {
                    {"id", id }, {"picture", $"Files/{id}.png"},
                    {"name", data[0] }, {"text", data[1]},
                    {"date", data[2] }
                });
            }
            return loader;
        }
        // сохранение
        public async Task<string> Save(HttpContext context)
        {
            var filePath = "Files";

            var post = new Post();
            post.Name = context.Request.Form["name"];
            post.Text = context.Request.Form["text"];
            post.Date = DateTime.Now.ToShortDateString();
            var fileCount = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories).Length;
            fileCount++;

            var validation = Validation.Validation.Validate(post);
            if (validation.IsValid)
            {
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
                var txtFileName = Path.Combine(filePath, fileCount + ".txt");
                File.AppendAllLines(txtFileName, new string[] { post.Name, post.Text, post.Date });
                return "Сохранено удачно!";
            }
            return validation.ErrMsg;
        }
        // удаление
        public void Remove(HttpContext context)
        {
            var id = context.Request.Path.Value.Split('/').Last();
            File.Delete($"Files/{id}.txt");
            File.Delete($"Files/{id}.png");
        }
        // редактирование
        public string Edit(HttpContext context)
        {
            var id = context.Request.Path.Value.Split('/').Last();
            var post = new Post();

            post.Name = context.Request.Form["name"];
            post.Text = context.Request.Form["text"];
            post.Date = DateTime.Now.ToShortDateString();
            var validation = Validation.Validation.Validate(post);

            if (validation.IsValid)
            {
                var txtFileName = Path.Combine("Files", id + ".txt");
                File.WriteAllLines(txtFileName, new string[] { post.Name, post.Text, post.Date });
                return "Отредактировано удачно!";
            }
            return validation.ErrMsg;
        }
    }
}
