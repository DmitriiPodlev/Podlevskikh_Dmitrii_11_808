using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vk.Validation;

namespace Vk.Models
{
    public class Post
    {
        [NotEmpty]
        [NotLong]
        public string Name { get; set; }
        [NotEmpty]
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
