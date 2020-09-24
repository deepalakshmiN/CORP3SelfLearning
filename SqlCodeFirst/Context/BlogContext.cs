using SqlCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SqlCodeFirst.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("testCsqldb") { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}