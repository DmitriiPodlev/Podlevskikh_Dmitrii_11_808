using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vk.Validation;

namespace Vk.Models
{
    public class Comment
    {
        [NotEmpty]
        [NotLong]
        public string Author { get; set; }
        [NotEmpty]
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
