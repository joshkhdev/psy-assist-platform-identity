using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.DatabaseProxyService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<ActionResult<IList<User>>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }

    // GET: api/Users/{username}
    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUserByUsername(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // GET: api/Users/ById/{userId}
    [HttpGet("ById/{userId}")]
    public async Task<ActionResult<User>> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.UserName }, user);
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    // PUT: api/Users/{username}
    [HttpPut("{username}")]
    public async Task<IActionResult> UpdateUser(string username, User updatedUser)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.IsBlocked = updatedUser.IsBlocked;
        user.UserName = updatedUser.UserName;
        user.PasswordHash = updatedUser.PasswordHash;
        // update other properties as necessary

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    // DELETE: api/Users/{username}
    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpGet("{userId}/roles")]
    public async Task<ActionResult<IList<string>>> GetRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }

    // POST: api/Users/{userId}/roles
    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AddToRole(string userId, [FromBody] string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            return BadRequest("Role does not exist.");
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    // DELETE: api/Users/{userId}/roles/{roleName}
    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }
}
