using FluentValidation;

namespace Target10._9.Business.Points.Commands;

public class AddPointCommand
{
    public float X_Coordinate { get; set; }
    public float Y_Coordinate { get; set; }
    public DateTime DateTimePoint { get; set; }
    
    public Guid SessionId { get; set; }
}

public class AddPointCommandValidator : AbstractValidator<AddPointCommand>
{
    public AddPointCommandValidator()
    {
        RuleFor(x => x.X_Coordinate)
            .NotEmpty()
            .WithMessage("X coordinates are required.");
        
        RuleFor(x => x.Y_Coordinate)
            .NotEmpty()
            .WithMessage("Y coordinates are required");
        
        RuleFor(x => x.DateTimePoint)
            .NotEmpty()
            .WithMessage("The date is required");
        
        RuleFor(x => x.SessionId)
            .NotEmpty()
            .WithMessage("Session is required.");
    }
}