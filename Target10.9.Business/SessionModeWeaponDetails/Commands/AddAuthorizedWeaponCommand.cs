using FluentValidation;

namespace Target10._9.Business.SessionModeWeaponDetails.Commands;

public class AddAuthorizedWeaponCommand
{
    public Guid WeaponDetailId { get; set; }
}

public class AddAuthorizedWeaponCommandValidator : AbstractValidator<AddAuthorizedWeaponCommand>
{
    public AddAuthorizedWeaponCommandValidator()
    {
        RuleFor(x => x.WeaponDetailId)
            .NotEmpty().WithMessage("Weapon Details ID is required.");
    }
}