using FluentValidation;

namespace Target10._9.Business.Users.Queries;

public class GetUserQuery
{
    public Guid Id { get; set; }
}

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID cannot be empty.");
    }
}