using AutoMapper;
using PsyAssistPlatformIdentity.Application.DTOs.User;
using PsyAssistPlatformIdentity.Application.Exceptions;
using PsyAssistPlatformIdentity.Application.Interfaces.Dto.User;
using PsyAssistPlatformIdentity.Application.Interfaces.Repository;
using PsyAssistPlatformIdentity.Application.Interfaces.Service;
using PsyAssistPlatformIdentity.Domain;

namespace PsyAssistPlatformIdentity.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    //private readonly IRepository<PsychologistProfile> _psychologistProfileRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IMapper _applicationMapper;
    private const string UserNotFoundMessage = "User with Id [{0}] not found";
    private const string ValuesCannotBeMessage = "Name, Email, Password values cannot be null or empty";
    private const string IncorrectEmailFormatMessage = "Incorrect email address format";
    private const string RoleNotFoundMessage = "Role with Id [{0}] not found";

    public UserService(
        IRepository<User> userRepository,
        //IRepository<PsychologistProfile> psychologistProfileRepository,
        IRepository<Role> roleRepository,
        IMapper applicationMapper)
    {
        _userRepository = userRepository;
        //_psychologistProfileRepository = psychologistProfileRepository;
        _roleRepository = roleRepository;
        _applicationMapper = applicationMapper;
    }

    public async Task<IEnumerable<IUser>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return _applicationMapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<IEnumerable<IUser>> GetActiveUsersAsync(CancellationToken cancellationToken)
    {
        var activeUsers = await _userRepository.GetAsync(user => user.IsBlocked == false, cancellationToken);
        return _applicationMapper.Map<IEnumerable<UserDto>>(activeUsers);
    }

    public async Task<IUser?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new NotFoundException(string.Format(UserNotFoundMessage, id));

        return _applicationMapper.Map<UserDto>(user);
    }

    public async Task<IUser> CreateUserAsync(ICreateUser userData, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userData.Name)
            || string.IsNullOrWhiteSpace(userData.Email)
            || string.IsNullOrWhiteSpace(userData.Password))
        {
            throw new IncorrectDataException(ValuesCannotBeMessage);
        }

        if (!Validator.EmailValidator(userData.Email))
            throw new IncorrectDataException(IncorrectEmailFormatMessage);

        var role = await _roleRepository.GetByIdAsync(userData.RoleId, cancellationToken);
        if (role is null)
            throw new IncorrectDataException(string.Format(RoleNotFoundMessage, userData.RoleId));

        var user = _applicationMapper.Map<User>(userData);
        user.IsBlocked = false;

        return _applicationMapper.Map<UserDto>(await _userRepository.AddAsync(user, cancellationToken));
    }

    public async Task<IUser> UpdateUserAsync(int id, IUpdateUser userData, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userData.Name)
            || string.IsNullOrWhiteSpace(userData.Email)
            || string.IsNullOrWhiteSpace(userData.Password))
        {
            throw new IncorrectDataException(ValuesCannotBeMessage);
        }

        if (!Validator.EmailValidator(userData.Email))
            throw new IncorrectDataException(IncorrectEmailFormatMessage);

        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new NotFoundException(string.Format(UserNotFoundMessage, id));

        var role = await _roleRepository.GetByIdAsync(userData.RoleId, cancellationToken);
        if (role is null)
            throw new IncorrectDataException(string.Format(RoleNotFoundMessage, userData.RoleId));

        //if (user.RoleId != userData.RoleId && userData.RoleId == (int)RoleType.Admin)
        //{
        //    var psychologistProfiles =
        //        await _psychologistProfileRepository.GetAsync(profile => profile.UserId == id, cancellationToken);
        //    if (psychologistProfiles.Any())
        //        throw new IncorrectDataException("User cannot be an admin, because he has a psychologist's profile");
        //}

        var updatedUser = _applicationMapper.Map<User>(userData);
        updatedUser.Id = id;
        updatedUser.IsBlocked = user.IsBlocked;

        return _applicationMapper.Map<UserDto>(await _userRepository.UpdateAsync(updatedUser, cancellationToken));
    }

    public async Task UnblockUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new NotFoundException(string.Format(UserNotFoundMessage, id));

        user.IsBlocked = false;

        //var psychologistProfiles =
        //    await _psychologistProfileRepository.GetAsync(profile => profile.UserId == id, cancellationToken);

        //var profilesList = psychologistProfiles.ToList();
        //if (profilesList.Count > 0)
        //{
        //    var psychologistProfile = profilesList.Single();
        //    psychologistProfile.IsActive = true;

        //    await _psychologistProfileRepository.UpdateAsync(psychologistProfile, cancellationToken);
        //}

        await _userRepository.UpdateAsync(user, cancellationToken);
    }

    public async Task BlockUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new NotFoundException(string.Format(UserNotFoundMessage, id));

        user.IsBlocked = true;

        //var psychologistProfiles =
        //    await _psychologistProfileRepository.GetAsync(profile => profile.UserId == id, cancellationToken);

        //var profilesList = psychologistProfiles.ToList();
        //if (profilesList.Count > 0)
        //{
        //    var psychologistProfile = profilesList.Single();
        //    psychologistProfile.IsActive = false;

        //    await _psychologistProfileRepository.UpdateAsync(psychologistProfile, cancellationToken);
        //}

        await _userRepository.UpdateAsync(user, cancellationToken);
    }

    Task<IEnumerable<UserDto>> IUserService.GetActiveUsersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<UserDto>> IUserService.GetAllUsersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<UserDto?> IUserService.GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<UserDto> IUserService.CreateUserAsync(ICreateUser userData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<UserDto> IUserService.UpdateUserAsync(int id, IUpdateUser userData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}