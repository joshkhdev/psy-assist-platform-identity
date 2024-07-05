using Microsoft.AspNetCore.Identity;

namespace PsyAssistPlatformIdentity.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public bool IsBlocked { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public int? PsychologistProfileId { get; set; }
    }
}
