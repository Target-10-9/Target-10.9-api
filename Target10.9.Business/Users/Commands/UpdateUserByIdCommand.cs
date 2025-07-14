using FluentValidation;

namespace Target10._9.Business.Users.Commands;

public class UpdateUserByIdCommand
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
}

public class UpdateUserByIdCommandValidator : AbstractValidator<UpdateUserByIdCommand>
{
    public UpdateUserByIdCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("L'email est requis")
            .EmailAddress()
            .WithMessage("L'email est invalide");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Le prénom est requis");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Le nom est requis");
        
        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .WithMessage("Le numéro de license est requis");
    }
}
