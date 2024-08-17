using PsyAssistPlatform.AuthService.Application.Interfaces.Dto.User;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.Application.Interfaces.Service;

public interface IUserService
{
    Task<User> RegisterUserAsync(ICreateUser model, CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetActiveUsersAsync(CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);

    Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken);

    Task<User> CreateUserAsync(ICreateUser userData, CancellationToken cancellationToken);

    Task<User> UpdateUserAsync(string id, IUpdateUser userData, CancellationToken cancellationToken);

    Task UnblockUserAsync(string id, CancellationToken cancellationToken);

    Task BlockUserAsync(string id, CancellationToken cancellationToken);
}