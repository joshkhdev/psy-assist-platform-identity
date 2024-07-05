using PsyAssistPlatformIdentity.Application.Interfaces.Dto.Role;

namespace PsyAssistPlatformIdentity.Application.Interfaces.Service;

public interface IRoleService
{
    Task<IEnumerable<IRole>> GetRolesAsync(CancellationToken cancellationToken);

    Task<IRole?> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
}