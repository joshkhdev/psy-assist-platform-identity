namespace PsyAssistPlatform.AuthService.Application.Interfaces.Dto.User;

public interface ICreateUser
{
    string Name { get; set; }

    string Email { get; set; }

    string Password { get; set; }

    int RoleId { get; set; }
}