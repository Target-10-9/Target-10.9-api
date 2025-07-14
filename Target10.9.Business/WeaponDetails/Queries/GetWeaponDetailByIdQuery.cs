using FluentValidation;

namespace Target10._9.Business.WeaponDetails.Queries;

public class GetWeaponDetailByIdQuery
{
    public Guid Id { get; set; }
}

public class GetWeaponDetailByIdQueryValidator : AbstractValidator<GetWeaponDetailByIdQuery>
{
    public GetWeaponDetailByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Weapon ID cannot be empty.");
    }
}