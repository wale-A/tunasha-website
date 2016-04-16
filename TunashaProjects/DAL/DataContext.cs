using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TunashaProjects.Models;

namespace TunashaProjects.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base ("DataContext"){}

        public DbSet<User> User { get; set; }
        public DbSet<PostedFile> File { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Post>().HasKey(x => x.)
        }
    }
}