using PsyAssistPlatform.AuthService.Application.Interfaces.Dto.User;

namespace PsyAssistPlatform.AuthService.IdentityService.Model.User;

public record CreateUserRequest : ICreateUser
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required int RoleId { get; set; }
}
