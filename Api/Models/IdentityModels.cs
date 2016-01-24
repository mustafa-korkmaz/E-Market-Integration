using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Api.DAL;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Api.DAL.DTO;

namespace Api.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser
    // class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<TestCodeFirstDto> TestCodeFirstDtos { get; set; }
        public DbSet<Market> Markets { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Api.DAL.DTO.Integration> Integrations { get; set; }

        public System.Data.Entity.DbSet<Api.DAL.DTO.MarketUser> MarketUsers { get; set; }

        public System.Data.Entity.DbSet<Api.DAL.DTO.MarketUserIntegration> MarketUserIntegrations { get; set; }
    }
}