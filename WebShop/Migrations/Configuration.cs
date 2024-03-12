namespace WebShop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebShop.Models.ApplicationDbContext context)
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
            //if (!context.Roles.Any(r => r.Name == "Administrator"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Administrator" };

            //    manager.Create(role);
            //}
            //if (!context.Users.Any(r => r.UserName == "Admin"))
            //{
            //    var store = new UserStore<User>(context);
            //    var manager = new UserManager<User>(store);
            //    var user = new User { UserName = "admin@admin.com", Email = "admin@admin.com", EmailConfirmed = true, LockoutEnabled = true };
                                                                         
            //    manager.Create(user, "admin123");
            //    manager.AddToRole(user.Id, "Administrator");
            //}

        }
    }
}
