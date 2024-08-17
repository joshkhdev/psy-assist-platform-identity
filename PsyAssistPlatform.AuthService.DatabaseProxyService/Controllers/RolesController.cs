using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.AuthService.DatabaseProxyService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // GET: api/Roles/ById/{id}
    [HttpGet("ById/{id}")]
    public async Task<ActionResult<IdentityRole>> GetRoleById(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return role;
    }

    // GET: api/Roles/ByName/{name}
    [HttpGet("ByName/{name}")]
    public async Task<ActionResult<IdentityRole>> GetRoleByName(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);

        if (role == null)
        {
            return NotFound();
        }

        return role;
    }

    // POST: api/Roles
    [HttpPost]
    public async Task<ActionResult<IdentityRole>> CreateRole(IdentityRole role)
    {
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
    }

    // PUT: api/Roles/{name}
    [HttpPut("{name}")]
    public async Task<IActionResult> UpdateRole(string name, IdentityRole role)
    {
        var existingRole = await _roleManager.FindByNameAsync(name);

        if (existingRole == null)
        {
            return NotFound();
        }

        existingRole.Name = role.Name;
        existingRole.NormalizedName = role.NormalizedName;
        existingRole.ConcurrencyStamp = role.ConcurrencyStamp;

        var result = await _roleManager.UpdateAsync(existingRole);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    // DELETE: api/Roles/{name}
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteRole(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);

        if (role == null)
        {
            return NotFound();
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    // GET: api/Roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }

    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AddToRole(string userId, [FromBody] string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpGet("{userId}/roles")]
    [HttpGet("api/Users/{userId}/roles")]
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

    [HttpGet("{userId}/roles/{roleName}")]
    public async Task<ActionResult<bool>> IsInRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(false);
        }

        var isInRole = await _userManager.IsInRoleAsync(user, roleName);

        return Ok(isInRole);
    }

    [HttpGet("{roleName}/users")]
    public async Task<ActionResult<IList<User>>> GetUsersInRole(string roleName)
    {
        var users = await _userManager.GetUsersInRoleAsync(roleName);
        return Ok(users);
    }
}
