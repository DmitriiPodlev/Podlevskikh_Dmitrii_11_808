using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vk.Models;

namespace Vk.Services
{
    public class CommentEntriesStorage : IStorage
    {
        // сохранение
        public async Task<string> Save(HttpContext context)
        {
            var comment = new Comment();
            var filePath = "Comments";
            comment.Author = context.Request.Form["author"];
            comment.Text = context.Request.Form["textarea"];
            comment.Date = DateTime.Now.ToShortDateString();

            var id = context.Request.Path.Value.Split('/').Last();
            var fileCount = Directory.GetFiles(filePath, String.Format("{0}.*.txt", id), SearchOption.AllDirectories).Length;
            fileCount++;

            var validation = Validation.Validation.Validate(comment);
            if (validation.IsValid)
            {
                foreach (var formFile in context.Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        string newFile = Path.Combine(filePath, id + "." + fileCount + Path.GetExtension(formFile.FileName));
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
                var txtFileName = Path.Combine(filePath, id + "." + fileCount + ".txt");
                File.AppendAllLines(txtFileName, new string[] { comment.Author, comment.Text, comment.Date });
                return "Сохранено удачно!";
            }
            return validation.ErrMsg;
        }
        // загрузка
        public List<Dictionary<string, string>> Load(HttpContext context)
        {
            string filePath = "Comments";

            var id = context.Request.Path.Value.Split('/').Last();
            string[] files = Directory.GetFiles(filePath, String.Format("{0}.*.txt", id), SearchOption.AllDirectories);
            var loader = new List<Dictionary<string, string>>();
            // заносим данные в loader
            foreach (var file in files)
            {
                var number = file.Substring(9);
                number = number.Substring(0, number.Length - 4);
                var data = File.ReadAllLines(file);
                loader.Add(new Dictionary<string, string>
                {
                    {"id", number}, {"author", data[0] }, 
                    {"text", data[1]}, {"date", data[2] }
                });
            }
            return loader;
        }
        // удаление 
        public void Remove(HttpContext context)
        {
            throw new NotImplementedException();
        }
        // редактирование
        public string Edit(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
