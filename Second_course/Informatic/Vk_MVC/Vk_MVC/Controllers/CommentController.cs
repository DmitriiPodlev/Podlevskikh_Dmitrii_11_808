using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vk_MVC.Models;

namespace Vk_MVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationContext context;

        public CommentController(ApplicationContext context)
        {
            this.context = context;
        }
        // GET: Comments
        public async Task<IActionResult> Index(int id)
        {
            var comments = await context.Comment
                .Include(user => user.Name)
                .Where(post => post.Post.Id == id)
                .ToListAsync();
            return View(comments);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var comment = await context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
                return NotFound();

            return View(comment);
        }

        // GET: Comment/Create
        public IActionResult Create(int id)
        {
            ViewBag.PostId = id;
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Name = context.User.FirstOrDefault(u => u.Name == User.Identity.Name);
                comment.Date = DateTime.Now.ToShortDateString();
                comment.Post = context.Post.FirstOrDefault(p => p.Id == id);
                context.Comment.Add(comment);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Post");
            }
            return View(comment);
        }
    }
}
