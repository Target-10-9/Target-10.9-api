using FluentValidation;

namespace Target10._9.Business.ModeDetails.Commands;

public class DeleteModeDetailByIdCommand
{
    public Guid Id { get; set; }
}

public class DeleteModeDetailByIdCommandValidator : AbstractValidator<DeleteModeDetailByIdCommand>
{
    public DeleteModeDetailByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'Id est requis");
    }
}