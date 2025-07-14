using FluentValidation;
using Target10._9.Business.Users.Repositories;

namespace Target10._9.Business.Authentications.Commands;

public class RegisterCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
}

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid")
            .MustAsync(async (email, cancellationToken) =>
                !await usersRepository.CheckIfEmailExistsAsync(email, cancellationToken))
            .WithMessage("Email not found");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must contain at least 6 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .WithMessage("License number is required");
    }
}