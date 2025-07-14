using FluentValidation;
using Target10._9.Business.Users.Repositories;

namespace Target10._9.Business.Authentications.Commands;

public class LoginCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid")
            .MustAsync(async (email, cancellationToken) =>
                await usersRepository.CheckIfEmailExistsAsync(email, cancellationToken))
            .WithMessage("Email not found");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must contain at least 6 characters");
    }
}