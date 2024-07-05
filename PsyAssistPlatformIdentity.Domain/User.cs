namespace PsyAssistPlatformIdentity.Domain;

/// <summary>
/// Пользователь
/// </summary>
public class User : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsBlocked { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

}