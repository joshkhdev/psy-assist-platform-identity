using Microsoft.AspNetCore.Identity;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.IdentityService;

public class CustomUserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>, IQueryableUserStore<User>
{
    private readonly ApiClient _apiClient;

    public IQueryable<User> Users => GetUsersQueryable();

    private IQueryable<User> GetUsersQueryable()
    {
        var users = _apiClient.GetAllUsersAsync().Result;
        return users.AsQueryable();
    }

    public CustomUserStore(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _apiClient.CreateUserAsync(user);
        return IdentityResult.Success;
    }

    public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await _apiClient.GetUserByUsernameAsync(normalizedUserName);
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        await _apiClient.UpdateUserAsync(user.UserName, user);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        await _apiClient.DeleteUserAsync(user.UserName);
        return IdentityResult.Success;
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash != null);
    }

    public void Dispose()
    {
        // Ничего не требуется
    }

    public Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return _apiClient.GetUserByIdAsync(userId);
    }

    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        await _apiClient.AddToRoleAsync(user, roleName);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        await _apiClient.RemoveFromRoleAsync(user, roleName);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
    {
        return await _apiClient.GetRolesAsync(user);
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        return await _apiClient.IsInRoleAsync(user, roleName);
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _apiClient.GetUsersInRoleAsync(roleName);
    }
}
