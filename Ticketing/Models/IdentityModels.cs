using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public string TelefoonNummer { get; set; }

        public string GsmNummer { get; set; }

       
       //public virtual ICollection<ApplicationUser> Gebruikers { get; set; }

        public bool? Actief { get; set; }

        public string VerantwoordelijkeId { get; set; }
        [ForeignKey("VerantwoordelijkeId")]
        public ApplicationUser Verantwoordelijke { get; set; }

        [InverseProperty("Verantwoordelijke")]
        public ICollection<ApplicationUser> Ondergeschikten { get; set; }




        [InverseProperty("CreationGebruiker")]
        public ICollection<Issue> IssuesCreated { get; set; }

        [InverseProperty("SolverGebruiker")]
        public ICollection<Issue> IssuesSolved { get; set; }
        



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //internal IEnumerable ApplicationUsers;

        public DbSet<LogInfo> Loginfo { get; set; }
        public DbSet<StatusInfo> StatusInfo { get; set; }
        public DbSet<PrioriteitInfo> PriorirteitInfo { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       //public System.Data.Entity.DbSet<Ticketing.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}