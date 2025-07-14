using FluentValidation;

namespace Target10._9.Business.SessionModes.Commands;

public class DeleteSessionModeByIdCommand
{
    public Guid Id { get; set; }
}

public class DeleteSessionModeByIdCommandValidator : AbstractValidator<DeleteSessionModeByIdCommand>
{
    public DeleteSessionModeByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'Id est requis");
    }
}