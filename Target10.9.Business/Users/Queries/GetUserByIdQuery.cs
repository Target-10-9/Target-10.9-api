using FluentValidation;

namespace Target10._9.Business.Users.Queries;

public class GetUserByIdQuery
{
    public Guid Id { get; set; }
}

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID cannot be empty.");
    }
}