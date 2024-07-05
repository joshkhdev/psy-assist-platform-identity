namespace PsyAssistPlatformIdentity.Application.Interfaces.Dto.User
{
    public interface IUser
    {
        int Id { get; set; }

        string Name { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        bool IsBlocked { get; set; }

        int RoleId { get; set; }
    }
}