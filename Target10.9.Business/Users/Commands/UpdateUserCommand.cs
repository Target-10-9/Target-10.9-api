using FluentValidation;

namespace Target10._9.Business.Users.Commands;

public class UpdateUserCommand
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
}

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("L'email est requis")
            .EmailAddress()
            .WithMessage("L'email est invalide");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Le prénom est requis");
    }
}
