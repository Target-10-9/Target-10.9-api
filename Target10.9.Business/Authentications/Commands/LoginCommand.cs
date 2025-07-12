using FluentValidation;

namespace Target10._9.Business.Authentications.Commands;

public class LoginCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("L'email est requis")
            .EmailAddress()
            .WithMessage("L'email est invalide");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Le mot de passe est requis")
            .MinimumLength(6)
            .WithMessage("Le mot de passe doit contenir au moins 6 caractères");
    }
}