using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Target10._9.Business.Accounts.Commands;

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
    public RegisterCommandValidator()
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

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Le prénom est requis");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Le nom est requis");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .WithMessage("Le numéro de licence est requis");
    }
}