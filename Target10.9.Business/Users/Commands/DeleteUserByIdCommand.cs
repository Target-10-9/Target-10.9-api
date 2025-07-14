using FluentValidation;

namespace Target10._9.Business.Users.Commands;

public class DeleteUserByIdCommand
{
    public Guid Id { get; set; }
}

public class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommand>
{
    public DeleteUserByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required.");
    }
}