using Microsoft.AspNetCore.Identity;
using PsyAssistPlatform.Application.Extensions;
using PsyAssistPlatform.AuthService.Application.Interfaces.Dto.User;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> RegisterUserAsync(ICreateUser model, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = model.Name,
                Email = model.Email,
                Name = model.Name,
                RoleType = (RoleType)model.RoleId,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            await _userManager.AddToRoleAsync(user, user.RoleType.ToDatabaseString());

            return user;
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync(CancellationToken cancellationToken)
        {
            return _userManager.Users.Where(u => !u.IsBlocked).ToList();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return _userManager.Users.ToList();
        }

        public async Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(ICreateUser userData, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = userData.Email,
                Email = userData.Email,
                Name = userData.Name
            };

            var result = await _userManager.CreateAsync(user, userData.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return user;
        }

        public async Task<User> UpdateUserAsync(string id, IUpdateUser userData, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new Exception("User not found");

            user.Name = userData.Name;
            user.Email = userData.Email;
            user.UserName = userData.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return user;
        }

        public async Task UnblockUserAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.IsBlocked = false;
            await _userManager.UpdateAsync(user);
        }

        public async Task BlockUserAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.IsBlocked = true;
            await _userManager.UpdateAsync(user);
        }
    }
}
