namespace PsyAssistPlatform.AuthService.WebApi.Controllers
{
    using global::PsyAssistPlatform.AuthService.Application.Interfaces.Service;
    using global::PsyAssistPlatform.AuthService.Domain;
    using global::PsyAssistPlatform.AuthService.WebApi.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

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
                    var user = await _userService.RegisterUserAsync(model, role,cancellationToken);
                    return Ok(new { user.Id, user.Email, user.Name });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }
        }
    }

}
