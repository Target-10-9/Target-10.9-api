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
            .WithMessage("Le nombre de tir limite est requis");
        
        RuleFor(x => x.ShootingTime)
            .NotEmpty()
            .WithMessage("Le temps de tir est requis");
        
        RuleFor(x => x.RestTime)
            .NotEmpty()
            .WithMessage("Le temps restant est requis");
    }
}