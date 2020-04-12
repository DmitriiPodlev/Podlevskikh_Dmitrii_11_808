using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk_MVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User Name { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public Post Post { get; set; }
    }
}
