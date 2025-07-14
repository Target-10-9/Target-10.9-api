using FluentValidation;

namespace Target10._9.Business.SessionModes.Commands;

public class AddSessionModeCommand
{
    public string Name { get; set; } = null!;
    public TimeOnly TimeLimits { get; set; }
    public TimeOnly WarmUp { get; set; }
    public string Discipline { get; set; } = null!;
    public Guid ModeDetailId { get; set; }
}

public class AddSessionModeCommandValidator : AbstractValidator<AddSessionModeCommand>
{
    public AddSessionModeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Le nom est requis");
        
        RuleFor(x => x.TimeLimits)
            .NotEmpty()
            .WithMessage("La limite de temps est requise");
        
        RuleFor(x => x.WarmUp)
            .NotEmpty()
            .WithMessage("Le warm up est requis");
        
        RuleFor(x => x.Discipline)
            .NotEmpty()
            .WithMessage("La discipline est requise");
        
        RuleFor(x => x.ModeDetailId)
            .NotEmpty()
            .WithMessage("Le détail du mode est requis");
    }
}