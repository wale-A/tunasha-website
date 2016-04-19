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

        public DbSet<User> Users { get; set; }
        public DbSet<PostedFile> Files { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Post>().HasKey(x => x.)
        }
    }
}