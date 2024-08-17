namespace PsyAssistPlatform.AuthService.IdentityService.Model.User;

public record UserResponse
{
    public string Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    //public string Password { get; set; } = null!;

    public bool IsBlocked { get; set; }

    public int RoleId { get; set; }
}