using FluentValidation;

namespace Target10._9.Business.Users.Commands;

public class DeleteUserCommand
{
    public Guid Id { get; set; }
}

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'ID de l'utilisateur est requis");
    }
}