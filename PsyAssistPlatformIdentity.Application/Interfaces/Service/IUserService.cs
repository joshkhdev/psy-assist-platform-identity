using PsyAssistPlatformIdentity.Application.DTOs.User;
using PsyAssistPlatformIdentity.Application.Interfaces.Dto.User;

namespace PsyAssistPlatformIdentity.Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetActiveUsersAsync(CancellationToken cancellationToken);

        Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<UserDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        Task<UserDto> CreateUserAsync(ICreateUser userData, CancellationToken cancellationToken);

        Task<UserDto> UpdateUserAsync(int id, IUpdateUser userData, CancellationToken cancellationToken);

        Task BlockUserAsync(int id, CancellationToken cancellationToken);
    }
}
