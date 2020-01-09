using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication;
using HtmlAgilityPack;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> logger;

        public async void Scanning(string url, int depth)
        {
            depth--;
            var web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//a");
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");
            var body = htmlBody.InnerHtml;
            foreach (var htmlNode in htmlNodes)
            {
                if (htmlNode.Attributes["href"] == null)
                    continue;
                var newHtmlNode = htmlNode.Attributes["href"].Value;
                var newUrl = "https://" + newHtmlNode;
                if (depth >= 0)
                {
                    SaveInformation(newUrl, body);
                    Scanning(newUrl, depth);
                }
            }

        }

        public static void SaveInformation(string url, string body)
        {
            using(TableContext db = new TableContext())
            {
                db.Table.Add(new Table { URL = url, Body = body});
                db.SaveChanges();
            }
        }

        private static string GetBody(HtmlNode adds)
        {
            string str = null;

            foreach (var add in adds.ChildNodes)
            {
                var div = add
                .ChildNodes
                .First(n => n.Name == "div");

                var title = div
                .InnerText;

                var cost = div
                .FirstChild
                .ChildNodes
                .First(n => n.Name == "span")
                .InnerText;
                cost = cost == " " ? "По договоренности" : cost;

                var href = add
                .ChildNodes
                .First(n => n.Name == "a")
                .Attributes["href"].Value;
                href = href != "Ссылка не опознана" ? "https://freelance.ru" + href : href;

                var date = add
                .ChildNodes
                .First(n => n.Name == "ul")
                .FirstChild
                .Attributes["title"].Value;

                var response = add
                .ChildNodes
                .First(n => n.Name == "ul")
                .ChildNodes[1]
                .InnerText;

                var viewsCount = add
                .ChildNodes
                .First(n => n.Name == "ul")
                .ChildNodes[2]
                .InnerText;

                str = str + date.ToString() + response.ToString() + viewsCount.ToString();
            }
            return str;
        }

        public static Table GetInformation(int id)
        {
            using(TableContext db = new TableContext())
            {
                var answer = db.Table.Find(id);
                return answer;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Domain, Depth")] Input input)
        {
            if (ModelState.IsValid)
            {
                //await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Save");
            }
            return View(input);
        }

        public IActionResult Index()
        {
            return View(new Input());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
