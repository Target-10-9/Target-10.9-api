using FluentValidation;

namespace Target10._9.Business.Sessions.Queries;

public class GetSessionByIdQuery
{
    public Guid Id { get; set; } 
}

public class GetSessionByIdQueryValidator : AbstractValidator<GetSessionByIdQuery>
{
    public GetSessionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Session ID cannot be empty.");
    }
}