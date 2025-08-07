using FluentValidation;

namespace Target10._9.Business.Points.Commands;

public class DeletePointByIdCommand
{
    public Guid Id { get; set; }
}

public class DeletePointByIdCommandValidator : AbstractValidator<DeletePointByIdCommand>
{
    public DeletePointByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required.");
    }
}