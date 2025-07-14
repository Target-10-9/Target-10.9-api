using FluentValidation;

namespace Target10._9.Business.Sessions.Commands;

public class DeleteSessionByIdCommand
{
    public Guid Id { get; set; }
}

public class DeleteSessionByIdCommandValidator : AbstractValidator<DeleteSessionByIdCommand>
{
    public DeleteSessionByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required.");
    }
}