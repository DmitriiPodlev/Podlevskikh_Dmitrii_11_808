using Microsoft.AspNetCore.Http;
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
        public List<Dictionary<string, string>> Load(HttpContext context)
        {
            throw new NotImplementedException();
        }
        public Task Save(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public void Remove(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public void Edit(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
