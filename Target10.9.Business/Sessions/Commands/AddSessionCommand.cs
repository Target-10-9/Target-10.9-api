using FluentValidation;

namespace Target10._9.Business.Sessions.Commands;

public class AddSessionCommand
{
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid SessionModeId { get; set; }
}

public class AddSessionCommandValidator : AbstractValidator<AddSessionCommand>
{
    public AddSessionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        
        RuleFor(x => x.DateStart)
            .NotEmpty()
            .WithMessage("The start date is required");
        
        RuleFor(x => x.DateEnd)
            .NotEmpty()
            .WithMessage("The end date is required");
        
        RuleFor(x => x.SessionModeId)
            .NotEmpty()
            .WithMessage("Session mode is required.");
    }
}