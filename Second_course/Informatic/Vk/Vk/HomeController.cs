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
        private readonly IStorage postStorage;
        private readonly IStorage commentStorage;

        public HomeController(Startup.ServiceResolver access)
        {
            postStorage = access("post");
            commentStorage = access("comment");
        }

        public async Task GetForm(HttpContext context)
        {
            await context.Response.WriteAsync(File.ReadAllText("Views\\Post.html"));
        }
        // сохранение
        public async Task AddEntry(HttpContext context)
        {
            await postStorage.Save(context);
            await context.Response.WriteAsync("New Entry was added");
        }
        // получение постов
        public async Task GetPosts(HttpContext context)
        {
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
            foreach (var file in postStorage.Load(context))
            {
                page.Append(String.Format(@" <div class = ""post"">
                    <h3><img src=""{1}"" ></h3>
                    <h4>{2}</h4>
                    <h5>{3}</h5>
                    <h5>{4}</h5>
                    <form action=""/Home/EditPost/{0}"" method=""get"" enctype=""multipart / form - data"" >
                        <input type=""submit"" value=""Редактировать""/><br /><br />
                    </form>
                    <form action=""/Home/RemovePost/{0}"" method=""post"" enctype=""multipart / form - data"" >
                        <input type=""submit"" value=""Удалить""/><br /><br />
                    </form>
                    <form action=""/Home/AddComment/{0}"" method=""get"" enctype=""multipart / form - data"" >
                        <input type=""submit"" value=""Добавить комментарий""/><br /><br />
                    </form>
                    <form action=""/Home/GetComments/{0}"" method=""get"" enctype=""multipart / form - data"" >
                        <input type=""submit"" value=""Показать комментарии""/>
                    </form>
                </div>", file["id"], file["picture"], file["name"], file["text"], file["date"]));
            }
            // добавив данные, завершаем html-код
            page.Append(@"</body>
            </html>");
            await context.Response.WriteAsync(page.ToString());
        }
        // удаление поста
        public async Task RemovePost(HttpContext context)
        {
            postStorage.Remove(context);
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
                postStorage.Edit(context);
                await GetPosts(context);
            }
        }
        // добавление комментария
        public async Task AddComment(HttpContext context)
        {
            var id = context.Request.Path.Value.Split('/').Last();
            if (context.Request.Method == "GET")
            {
                var page = String.Format(@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta charset=""utf-8"" />
                        <title>Придумай комментарий!</title>
                    </head>
                    <body>
                        <form action=""/Home/AddComment/{0}"" method=""post"" enctype=""multipart/form-data"" >
                            Автор: <input name=""author"" /><br /><br />
                            Текст: <textarea name=""textarea""></textarea><br /><br />
                            <input type=""submit"" value=""Отправить""/>
                        </form>
                    </body>
               </html> ", id);
                await context.Response.WriteAsync(page);
            }

            else
            {
                await commentStorage.Save(context);
                await GetPosts(context);
            }
        }
        // получение комментариев
        public async Task GetComments(HttpContext context)
        {
            var page = new StringBuilder();
            page.Append(@"<!DOCTYPE html>
            <html>
                <head>
                    <meta charset=""utf-8"" />
                    <title>Комментарии:</title>
                </head>
                <body>
            ");
            // проходимся по файлам
            foreach (var file in commentStorage.Load(context))
            {
                page.Append(String.Format(@" <div class = ""comment"">
                    <h1>Номер комментария: {0}<h1>
                    <h3>Автор:<h3>
                    <h4>{1}</h4>
                    <h3>Текст:<h3>
                    <h5>{2}</h5>
                    <h3>Дата:<h3>
                    <h5>{3}</h5>
                </div>", file["id"], file["author"], file["text"], file["date"]));
            }
            // добавив данные, завершаем html-код
            page.Append(@"
                    <form action=""/Home/GetAllPosts"" method=""get"" enctype=""multipart/form-data"" >
                    <input type=""submit"" value=""Назад""/>
                    </form>
                </body>
            </html>");
            await context.Response.WriteAsync(page.ToString());
        }
    }
}