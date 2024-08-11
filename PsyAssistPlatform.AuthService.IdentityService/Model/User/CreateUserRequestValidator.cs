using FluentValidation;

namespace PsyAssistPlatform.AuthService.IdentityService.Model.User;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name value cannot be null or empty");
        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email value cannot be null or empty")
            .EmailAddress()
            .WithMessage("Incorrect email address format");
        RuleFor(request => request.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password value cannot be null or empty");
    }
}