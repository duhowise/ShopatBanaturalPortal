namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopatBanaturalPortal.Models.InventoryItemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        bool AddUserAndRole(ShopatBanaturalPortal.Models.ApplicationDbContext context)
        {
            //adds can edit role, and admin user using application dbcontext
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("CanView"));
            ir = rm.Create(new IdentityRole("CanEdit"));
            ir = rm.Create(new IdentityRole("CanManageUsers"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser() { UserName = "TestAccount@codingisfun.com", };

            //create new user with username and password using usermanager and add to app dbcontext
            ir = um.Create(user, "Mypassword1!");
            if (ir.Succeeded == false)
                return ir.Succeeded;

            ir = um.AddToRole(user.Id, "CanView");
            ir = um.AddToRole(user.Id, "CanEdit");
            ir = um.AddToRole(user.Id, "CanManageUsers");
            return ir.Succeeded;
        }

        protected override void Seed(ShopatBanaturalPortal.Models.InventoryItemDbContext context)
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
        }
    }
}
