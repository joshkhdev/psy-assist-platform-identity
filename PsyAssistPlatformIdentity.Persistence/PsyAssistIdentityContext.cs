using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsyAssistPlatformIdentity.Domain;

namespace PsyAssistPlatformIdentity.Persistence;

public class PsyAssistIdentityContext : IdentityDbContext<ApplicationUser>
{
    public PsyAssistIdentityContext(DbContextOptions options) : base(options)
    {
    }
}
