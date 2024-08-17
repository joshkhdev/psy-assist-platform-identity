using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;
using PsyAssistPlatform.AuthService.IdentityService.Model.User;

namespace PsyAssistPlatform.AuthService.IdentityService.Conrtollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.RegisterUserAsync(model, cancellationToken);
                return Ok(new { user.Id, user.Email, user.Name });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Получить список действующих пользователей
        /// </summary>
        [HttpGet()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetActiveUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userService.GetActiveUsersAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            if (await _userService.GetAllUsersAsync(cancellationToken) is { } activeUsers)
            {
                return Ok(activeUsers.Select(user => new UserResponse()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                    RoleId = (int)user.RoleType,
                }));
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Получить пользователя по Id
        /// </summary>
        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetUserByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(id, cancellationToken) is { } user)
            {
                return Ok(new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                    RoleId = (int)user.RoleType
                });
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest createRequest, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(createRequest, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(string id, UpdateUserRequest updateRequest, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(id, updateRequest, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Разблокировать пользователя
        /// </summary>
        [HttpPut("{id:Guid}/unblock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnblockUserAsync(string id, CancellationToken cancellationToken)
        {
            await _userService.UnblockUserAsync(id, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUserAsync(string id, CancellationToken cancellationToken)
        {
            await _userService.BlockUserAsync(id, cancellationToken);
            return Ok();
        }
    }
}
