using FluentValidation;
using Target10._9.Business.Sessions.Queries;

namespace Target10._9.Business.Points.Queries;

public class GetPointsBySessionIdQuery
{
    public Guid Id { get; set; } 
}

public class GetPointByIdQueryValidator : AbstractValidator<GetPointsBySessionIdQuery>
{
    public GetPointByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Point ID cannot be empty.");
    }
}