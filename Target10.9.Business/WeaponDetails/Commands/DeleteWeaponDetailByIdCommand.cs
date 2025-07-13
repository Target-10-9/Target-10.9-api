using FluentValidation;

namespace Target10._9.Business.WeaponDetails.Commands;

public class DeleteWeaponDetailByIdCommand
{
    public Guid Id { get; set; }
}

public class DeleteWeaponDetailByIdCommandValidator : AbstractValidator<DeleteWeaponDetailByIdCommand>
{
    public DeleteWeaponDetailByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'ID de l'utilisateur est requis");
    }
}