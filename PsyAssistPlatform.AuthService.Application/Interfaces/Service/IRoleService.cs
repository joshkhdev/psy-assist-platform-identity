using Microsoft.AspNetCore.Identity;

namespace PsyAssistPlatform.AuthService.Application.Interfaces.Service;

public interface IRoleService
{
    Task<IEnumerable<IdentityRole>> GetRolesAsync(CancellationToken cancellationToken);

    Task<IdentityRole?> GetRoleByIdAsync(string id, CancellationToken cancellationToken);
}