using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;
using PsyAssistPlatform.AuthService.Domain;
using PsyAssistPlatform.AuthService.WebApi.Models;

namespace PsyAssistPlatform.AuthService.WebApi.Controllers
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
        public async Task<IActionResult> Register([FromBody] CreateUserModel model, [FromQuery] RoleType role, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.RegisterUserAsync(model, role, cancellationToken);
                return Ok(new { user.Id, user.Email, user.Name });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("active")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetActiveUsersAsync(CancellationToken cancellationToken)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            try
            {
                var users = await _userService.GetActiveUsersAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
