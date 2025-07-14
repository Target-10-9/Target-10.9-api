using FluentValidation;

namespace Target10._9.Business.ModeDetails.Queries;

public class GetModeDetailByIdQuery
{
    public Guid Id { get; set; }
}

public class GetModeDetailByIdQueryValidator : AbstractValidator<GetModeDetailByIdQuery>
{
    public GetModeDetailByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Weapon detail cannot be empty.");
    }
}