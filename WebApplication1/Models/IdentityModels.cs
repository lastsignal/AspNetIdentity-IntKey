using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{

    public class ApplicationIdentityUserLogin:IdentityUserLogin<int>
    {
    }

    public class ApplicationIdentityUserRole:IdentityUserRole<int>
    {
    }

    public class ApplicationIdentityUserClaim:IdentityUserClaim<int>
    {
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>
    {
        public string AwesomeInformation { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Awesome Type", "Awesome Value"));

            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole<int, ApplicationIdentityUserRole>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "security");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles", "security");
            modelBuilder.Entity<ApplicationIdentityUserRole>().ToTable("UserRoles", "security");
            modelBuilder.Entity<ApplicationIdentityUserLogin>().ToTable("UserLogins", "security");
            modelBuilder.Entity<ApplicationIdentityUserClaim>().ToTable("UserClaims", "security");

            modelBuilder.Entity<ApplicationUser>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationRole>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationIdentityUserClaim>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class ApplicationUserStore : UserStore
            <ApplicationUser, ApplicationRole, int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole,
                ApplicationIdentityUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationIdentityUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context) : base(context) { }
    }
}
