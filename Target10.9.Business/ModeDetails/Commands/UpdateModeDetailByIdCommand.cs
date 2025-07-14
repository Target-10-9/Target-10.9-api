using FluentValidation;

namespace Target10._9.Business.ModeDetails.Commands;

public class UpdateModeDetailByIdCommand
{
    public int ShootLimit { get; set; }
    public TimeOnly ShootingTime { get; set; }
    public TimeOnly RestTime { get; set; }
}

public class UpdateModeDetailByIdCommandValidator : AbstractValidator<UpdateModeDetailByIdCommand>
{
    public UpdateModeDetailByIdCommandValidator()
    {
        RuleFor(x => x.ShootLimit)
            .NotEmpty()
            .WithMessage("The limit number of shots is required");
        
        RuleFor(x => x.ShootingTime)
            .NotEmpty()
            .WithMessage("Shooting time is required");
        
        RuleFor(x => x.RestTime)
            .NotEmpty()
            .WithMessage("Rest time is required");
    }
}