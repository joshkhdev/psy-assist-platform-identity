using Microsoft.AspNetCore.Identity;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.IdentityService;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> CreateUserAsync(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users", user);
        return response.EnsureSuccessStatusCode();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var response = await _httpClient.GetAsync($"api/Users/{username}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task DeleteUserAsync(string? userName)
    {
        var response = await _httpClient.DeleteAsync($"api/Users/{userName}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateUserAsync(string? userName, User user)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Users/{userName}", user);
        response.EnsureSuccessStatusCode();
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"api/Users/ById/{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task CreateRoleAsync(IdentityRole role)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Roles", role);
        response.EnsureSuccessStatusCode(); // Если сервер вернет ошибку, выбросится исключение
    }

    public async Task DeleteRoleAsync(string name)
    {
        var response = await _httpClient.DeleteAsync($"api/Roles/{name}");
        response.EnsureSuccessStatusCode(); // Если сервер вернет ошибку, выбросится исключение
    }

    public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
    {
        var response = await _httpClient.GetAsync($"api/Roles/ById/{roleId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IdentityRole>();
        }

        return null; // Или выбросить исключение, если роль не найдена
    }

    public async Task<IdentityRole> GetRoleByNameAsync(string normalizedRoleName)
    {
        var response = await _httpClient.GetAsync($"api/Roles/ByName/{normalizedRoleName}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IdentityRole>();
        }

        return null; // Или выбросить исключение, если роль не найдена
    }

    public async Task UpdateRoleAsync(string name, IdentityRole role)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Roles/{name}", role);
        response.EnsureSuccessStatusCode(); // Если сервер вернет ошибку, выбросится исключение
    }

    public async Task<IdentityRole[]> GetAllRolesAsync()
    {
        var response = await _httpClient.GetAsync("api/Roles");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IdentityRole[]>();
    }

    public async Task AddToRoleAsync(User user, string roleName)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Users/{user.Id}/roles", new { RoleName = roleName });
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveFromRoleAsync(User user, string roleName)
    {
        var response = await _httpClient.DeleteAsync($"api/Users/{user.Id}/roles/{roleName}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        var response = await _httpClient.GetAsync($"api/Users/{user.Id}/roles");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IList<string>>();
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName)
    {
        var response = await _httpClient.GetAsync($"api/Users/{user.Id}/roles/{roleName}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
    {
        var response = await _httpClient.GetAsync($"api/Roles/{roleName}/users");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IList<User>>();
    }

    public async Task<IList<User>> GetAllUsersAsync()
    {
        var response = await _httpClient.GetAsync("api/Users");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IList<User>>();
    }
}
