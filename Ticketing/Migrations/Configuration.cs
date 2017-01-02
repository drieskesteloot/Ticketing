namespace Ticketing.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;
    internal sealed class Configuration : DbMigrationsConfiguration<Ticketing.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ticketing.Models.ApplicationDbContext context)
        {
            context.Database.Delete();
            context.Database.Create();
            context.PriorirteitInfo.AddOrUpdate(p => p.Omschrijving,
                new Models.PrioriteitInfo { Omschrijving = "Laag" }, 
                new Models.PrioriteitInfo { Omschrijving = "Gemiddeld"},
                new Models.PrioriteitInfo { Omschrijving = "Hoog" },
                new Models.PrioriteitInfo { Omschrijving = "Dringend"});

            context.StatusInfo.AddOrUpdate(p => p.Omschrijving, 
                new Models.StatusInfo { Omschrijving = "Nieuw"},
                new Models.StatusInfo { Omschrijving = "Toegewezen"},
                new Models.StatusInfo { Omschrijving = "In behandeling"},
                new Models.StatusInfo { Omschrijving = "Opgelost"},
                new Models.StatusInfo { Omschrijving = "Afgesloten"},
                new Models.StatusInfo { Omschrijving = "Canceled"}, 
                new Models.StatusInfo { Omschrijving = "ExtraInfo"},
                new Models.StatusInfo { Omschrijving = "Geweigerd"});

            var PasswordHash = new PasswordHasher();
            context.Users.AddOrUpdate(p => p.Voornaam,
                new Models.ApplicationUser { Voornaam = "admin", Achternaam = "admin",
                     TelefoonNummer = "123",
                    GsmNummer = "0444/55.66.77", Actief = true, UserName = "admin@admin.com",
                    PasswordHash = PasswordHash.HashPassword("Test_1234"),
                    Email = "admin@admin.com", SecurityStamp = Guid.NewGuid().ToString()});

            

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser
            {
                Voornaam = "admin",
                Achternaam = "admin",
                TelefoonNummer = "123",
                GsmNummer = "0444/55.55.66.77",
                Actief = true,
                UserName = "admin2@admin.com",
                PasswordHash = PasswordHash.HashPassword("Test_1234"),
                Email = "admin2@admin.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            manager.Create(user);
            //manager.AddToRole(user.Id, Const)
            manager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Administrator"));
            manager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "User"));
            context.SaveChanges();
            
            


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
