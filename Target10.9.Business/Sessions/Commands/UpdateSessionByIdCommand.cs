using FluentValidation;

namespace Target10._9.Business.Sessions.Commands;

public class UpdateSessionByIdCommand
{
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid SessionModeId { get; set; }
}

public class UpdateSessionByIdCommandValidator : AbstractValidator<UpdateSessionByIdCommand>
{
    public UpdateSessionByIdCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Le nom est requis");
        
        RuleFor(x => x.DateStart)
            .NotEmpty()
            .WithMessage("La Date de commencement est requise");
        
        RuleFor(x => x.DateEnd)
            .NotEmpty()
            .WithMessage("La date de fin est requise");
        
        RuleFor(x => x.SessionModeId)
            .NotEmpty()
            .WithMessage("La session est requise");
    }
}