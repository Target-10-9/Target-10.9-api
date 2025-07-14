using FluentValidation;

namespace Target10._9.Business.WeaponDetails.Commands;

public class UpdateWeaponDetailByIdCommand
{
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
}

public class UpdateWeaponDetailByIdCommandValidator : AbstractValidator<UpdateWeaponDetailByIdCommand>
{
    public UpdateWeaponDetailByIdCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("The brand is required");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        
        RuleFor(x => x.SerialNumber)
            .NotEmpty()
            .WithMessage("Serial number is required");
    }
}