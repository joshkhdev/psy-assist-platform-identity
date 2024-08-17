using Microsoft.AspNetCore.Mvc;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;
using PsyAssistPlatform.AuthService.IdentityService.Model.Role;

namespace PsyAssistPlatform.AuthService.IdentityService.Conrtollers;

/// <summary>
/// Роли пользователей
/// </summary>
[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Получить список ролей
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetRolesAsync(cancellationToken);
        return Ok(roles.Select(r => new RoleResponse
        {
            Id = r.Id,
            Name = r.Name,
        }));

        //return _mapper.Map<IEnumerable<RoleResponse>>(roles);
    }

    /// <summary>
    /// Получить роль по Id
    /// </summary>
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<RoleResponse>> GetRoleByIdAsync(string id, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);
        return Ok(role);
        //return _mapper.Map<RoleResponse>(role);
    }
}
