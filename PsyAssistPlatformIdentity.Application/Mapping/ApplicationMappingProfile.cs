using AutoMapper;
using PsyAssistPlatformIdentity.Application.DTOs;
using PsyAssistPlatformIdentity.Application.DTOs.Role;
using PsyAssistPlatformIdentity.Application.DTOs.User;
using PsyAssistPlatformIdentity.Application.Interfaces.Dto.User;
using PsyAssistPlatformIdentity.Domain;

namespace PsyAssistPlatform.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        //CreateApproachMap();
        //CreateContactMap();
        //CreatePsychologistProfileMap();
        CreateRoleMap();
        //CreateStatusMap();
        //CreateQuestionnaireMap();
        CreateUserMap();
    }

    //private void CreateApproachMap()
    //{
    //    CreateMap<Approach, ApproachDto>();
    //    CreateMap<ICreateApproach, Approach>();
    //    CreateMap<IUpdateApproach, Approach>();
    //}

    //private void CreateContactMap()
    //{
    //    CreateMap<Contact, ContactDto>();
    //    CreateMap<IUpdateContact, Contact>();
    //}

    //private void CreatePsychologistProfileMap()
    //{
    //    CreateMap<PsychologistProfile, PsychologistProfileDto>();
    //    CreateMap<ICreatePsychologistProfile, PsychologistProfile>();
    //    CreateMap<IUpdatePsychologistProfile, PsychologistProfile>();
    //}

    //private void CreateQuestionnaireMap()
    //{
    //    CreateMap<Questionnaire, QuestionnaireDto>();
    //    CreateMap<ICreateQuestionnaire, Questionnaire>();
    //}

    private void CreateRoleMap()
    {
        CreateMap<Role, RoleDto>();
    }

    //private void CreateStatusMap()
    //{
    //    CreateMap<Status, StatusDto>();
    //}

    private void CreateUserMap()
    {
        CreateMap<User, UserDto>();
        CreateMap<ICreateUser, User>();
        CreateMap<IUpdateUser, User>();
    }
}