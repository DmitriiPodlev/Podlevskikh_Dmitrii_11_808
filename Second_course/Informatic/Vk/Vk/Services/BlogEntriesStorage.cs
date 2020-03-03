using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vk.Models;

namespace Vk.Services
{
    public class BlogEntriesStorage : IStorage
    {
        public IEnumerable<Post> BlogEntries { get; set; }
        public void Load()
        {

        }
        public void Save()
        {

        }
    }
}
