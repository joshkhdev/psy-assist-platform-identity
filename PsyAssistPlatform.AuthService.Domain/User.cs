using Microsoft.AspNetCore.Identity;

namespace PsyAssistPlatform.AuthService.Domain;

/// <summary>
/// Пользователь
/// </summary>
public class User : IdentityUser
{
    public string Name { get; set; } = null!;

    public bool IsBlocked { get; set; }

    public RoleType RoleType { get; set; }
}