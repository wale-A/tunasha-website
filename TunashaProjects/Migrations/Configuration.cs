namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TunashaProjects.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TunashaProjects.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TunashaProjects.DAL.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.User.AddOrUpdate(
                p => p.Name,
                new User
                {
                    Name = "admin",
                    Email = "admin@123.co.uk",
                    Password = "admin123"
                });
        }
    }
}
