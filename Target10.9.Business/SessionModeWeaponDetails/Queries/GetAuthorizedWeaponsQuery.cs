using FluentValidation;

namespace Target10._9.Business.SessionModeWeaponDetails.Queries;

public class GetAuthorizedWeaponsQuery
{
    public Guid Id { get; set; }
}

public class GetAuthorizedWeaponsQueryValidator : AbstractValidator<GetAuthorizedWeaponsQuery>
{
    public GetAuthorizedWeaponsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required.");
    }
}