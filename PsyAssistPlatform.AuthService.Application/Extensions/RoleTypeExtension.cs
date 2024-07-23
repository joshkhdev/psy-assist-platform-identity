using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.Application.Extensions;

public static class RoleTypeExtension
{
    public static string ToDatabaseString(this RoleType role)
    {
        return role switch
        {
            RoleType.Admin => "Admin",
            RoleType.Curator => "Curator",
            RoleType.Psychologist => "Psychologist",
            _ => throw new ArgumentException("Role is not defined in the database")
        };
    }
    
    public static RoleType ToRoleTypeEnum(this string role)
    {
        return role switch
        {
            "Admin" => RoleType.Admin,
            "Curator" => RoleType.Curator,
            "Psychologist" => RoleType.Psychologist,
            _ => throw new ArgumentException("Role is not defined in the RoleType enum")
        };
    }
}