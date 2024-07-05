using PsyAssistPlatformIdentity.Application.Interfaces.Dto.Role;

namespace PsyAssistPlatformIdentity.Application.DTOs.Role;

public record RoleDto : IRole
{
    public int Id { get; set; }

    public required string Name { get; set; }
}