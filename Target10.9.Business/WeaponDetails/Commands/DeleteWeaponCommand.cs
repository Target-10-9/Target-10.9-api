using FluentValidation;

namespace Target10._9.Business.WeaponDetails.Commands;

public class DeleteWeaponCommand
{
    public Guid Id { get; set; }
}

public class DeleteWeaponCommandValidator : AbstractValidator<DeleteWeaponCommand>
{
    public DeleteWeaponCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'ID de l'utilisateur est requis");
    }
}