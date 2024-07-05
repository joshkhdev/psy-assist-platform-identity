using Microsoft.EntityFrameworkCore;
using PsyAssistPlatformIdentity.Domain;
using PsyAssistPlatformIdentity.Persistence.EntityTypeConfigurations;

namespace PsyAssistPlatformIdentity.Persistence;

public class PsyAssistContext : DbContext
{
    public PsyAssistContext(DbContextOptions<PsyAssistContext> options) : base(options)
    {
    }

    //public DbSet<Approach> Approaches { get; set; }

    //public DbSet<Contact> Contacts { get; set; }

    //public DbSet<PsychologistProfile> Psychologists { get; set; }

    //public DbSet<Questionnaire> Questionnaires { get; set; }

    public DbSet<Role> Roles { get; set; }

    //public DbSet<Status> Statuses { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            //.ApplyConfiguration(new ApproachConfiguration())
            //.ApplyConfiguration(new ContactConfiguration())
            //.ApplyConfiguration(new PsychologistProfileConfiguration())
            //.ApplyConfiguration(new QuestionnaireConfiguration())
            .ApplyConfiguration(new RoleConfiguration())
            //.ApplyConfiguration(new StatusConfiguration())
            .ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCamelCaseNamingConvention();
}