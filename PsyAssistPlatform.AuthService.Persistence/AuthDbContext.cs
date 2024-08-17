using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.Persistence
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        //public DbSet<IdentityRole> Roles { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseCamelCaseNamingConvention();
    }
}
