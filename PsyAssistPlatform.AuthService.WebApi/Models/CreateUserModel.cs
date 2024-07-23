using PsyAssistPlatform.AuthService.Application.Interfaces.Dto.User;

namespace PsyAssistPlatform.AuthService.WebApi.Models
{
    public class CreateUserModel :ICreateUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
