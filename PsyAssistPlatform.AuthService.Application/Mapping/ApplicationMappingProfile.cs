using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PsyAssistPlatform.AuthService.Application.Dto.Role;
using PsyAssistPlatform.AuthService.Application.Dto.User;
using PsyAssistPlatform.AuthService.Application.Interfaces.Dto.User;
using PsyAssistPlatform.AuthService.Domain;

namespace PsyAssistPlatform.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateRoleMap();
        CreateUserMap();
    }

    private void CreateRoleMap()
    {
        CreateMap<IdentityRole, RoleDto>();
    }

    private void CreateUserMap()
    {
        CreateMap<User, UserDto>();
        CreateMap<ICreateUser, User>();
        CreateMap<IUpdateUser, User>();
    }
}