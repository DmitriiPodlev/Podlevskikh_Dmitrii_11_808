using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication
{
    public class Table
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Body { get; set; }
    }

    public class Input
    {
        public string Domain { get; set; }
        public int Depth { get; set; }
        public int DomainId { get; set; }
    }

    public class TableContext : DbContext
    {
        public DbSet<Table> Table { get; set; }

        public TableContext()
        {
            Database.EnsureCreated();
        }
    }
}
