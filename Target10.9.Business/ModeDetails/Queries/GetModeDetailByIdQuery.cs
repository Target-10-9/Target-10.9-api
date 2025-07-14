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
            .WithMessage("Le détail de l'arme ne peux pas être vide.");
    }
}