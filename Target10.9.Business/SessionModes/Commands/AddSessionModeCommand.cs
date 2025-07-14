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
            .WithMessage("Name is required");
        
        RuleFor(x => x.TimeLimits)
            .NotEmpty()
            .WithMessage("Time limit is required");
        
        RuleFor(x => x.WarmUp)
            .NotEmpty()
            .WithMessage("Warm-up is required");
        
        RuleFor(x => x.Discipline)
            .NotEmpty()
            .WithMessage("Discipline is required");
        
        RuleFor(x => x.ModeDetailId)
            .NotEmpty()
            .WithMessage("Mode detail is required");
    }
}