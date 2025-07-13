using FluentValidation;

namespace Target10._9.Business.WeaponDetails.Commands;

public class AddWeaponDetailCommand
{
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
}

public class WeaponDetailCommandValidator : AbstractValidator<AddWeaponDetailCommand>
{
    public WeaponDetailCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Le nom est requis");
        
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("La marque est requise");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("La description est requis");
        
        RuleFor(x => x.SerialNumber)
            .NotEmpty()
            .WithMessage("Le numero de série est requis");
    }
}