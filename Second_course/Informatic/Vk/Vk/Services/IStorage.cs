using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk.Services
{
    public interface IStorage
    {
        public Task Save(HttpContext context);
        public List<Dictionary<string, string>> Load(HttpContext context);
        public void Remove(HttpContext context);
        public void Edit(HttpContext context);
    }
}
