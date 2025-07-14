using FluentValidation;

namespace Target10._9.Business.Logs.Queries;

public class GetLogByIdQuery
{
    public Guid Id { get; set; }
}

public class GetLogByIdQueryValidator : AbstractValidator<GetLogByIdQuery>
{
    public GetLogByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required.");
    }
}