namespace PsyAssistPlatform.AuthService.IdentityService.Model.Role;

public record RoleResponse
{
    public string Id { get; set; }

    public string Name { get; set; } = null!;
}