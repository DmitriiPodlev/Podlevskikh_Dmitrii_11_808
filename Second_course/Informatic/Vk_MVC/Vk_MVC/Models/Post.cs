using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk_MVC.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
