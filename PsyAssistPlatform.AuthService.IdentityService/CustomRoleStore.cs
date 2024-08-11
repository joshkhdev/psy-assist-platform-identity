using Microsoft.AspNetCore.Identity;

namespace PsyAssistPlatform.AuthService.IdentityService
{
    public class CustomRoleStore : IRoleStore<IdentityRole>, IQueryableRoleStore<IdentityRole>
    {
        private readonly ApiClient _apiClient;

        public IQueryable<IdentityRole> Roles => GetRolesQueryable();

        private IQueryable<IdentityRole> GetRolesQueryable()
        {
            var roles = _apiClient.GetAllRolesAsync().Result;
            return roles.AsQueryable();
        }

        public CustomRoleStore(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await _apiClient.CreateRoleAsync(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await _apiClient.DeleteRoleAsync(role.Name);
            return IdentityResult.Success;
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _apiClient.GetRoleByIdAsync(roleId);
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _apiClient.GetRoleByNameAsync(normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await _apiClient.UpdateRoleAsync(role.Name, role);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // Не требуется освобождение ресурсов
        }
    }
}
