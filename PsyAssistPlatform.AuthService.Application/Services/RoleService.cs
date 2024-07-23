using Microsoft.AspNetCore.Identity;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;

namespace PsyAssistPlatform.AuthService.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync(CancellationToken cancellationToken)
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<IdentityRole?> GetRoleByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _roleManager.FindByIdAsync(id);
        }
    }
}
