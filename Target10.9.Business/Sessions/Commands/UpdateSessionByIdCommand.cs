using FluentValidation;

namespace Target10._9.Business.Sessions.Commands;

public class UpdateSessionByIdCommand
{
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid SessionModeId { get; set; }
    public SessionEtat Etat { get; set; }
}

public class UpdateSessionByIdCommandValidator : AbstractValidator<UpdateSessionByIdCommand>
{
    public UpdateSessionByIdCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(x => x.DateStart)
            .NotEmpty()
            .WithMessage("The start date is required");
        
        RuleFor(x => x.DateEnd)
            .NotEmpty()
            .WithMessage("The end date is required");
        
        RuleFor(x => x.SessionModeId)
            .NotEmpty()
            .WithMessage("The session mode is required");
        
        RuleFor(x => x.Etat)
            .IsInEnum()
            .WithMessage("Etat must be a valid SessionEtat value");
    }
}