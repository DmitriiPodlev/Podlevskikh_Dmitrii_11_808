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
        public int Body { get; set; }
    }

    public class Information
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public int Depth { get; set; }
    }
}

//    public class TableContext : DbContext
//    {
//        public TableContext()
//: base("DbConnection")
//        {

//        }
//        public DbSet<Table> Table { get; set; }
//        public DbSet<Information> Information { get; set; }
//    }
//}
