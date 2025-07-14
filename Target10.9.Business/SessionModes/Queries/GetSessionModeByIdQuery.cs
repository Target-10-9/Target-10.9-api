using FluentValidation;

namespace Target10._9.Business.SessionModes.Queries;

public class GetSessionModeByIdQuery
{
    public Guid Id { get; set; }
}

public class GetSessionModeByIdQueryValidator : AbstractValidator<GetSessionModeByIdQuery>
{
    public GetSessionModeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'id est requis.");
    }
}