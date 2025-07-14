using FluentValidation;

namespace Target10._9.Business.Users.Commands;

public class UpdateUserByIdCommand
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
}

public class UpdateUserByIdCommandValidator : AbstractValidator<UpdateUserByIdCommand>
{
    public UpdateUserByIdCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email est requis")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");
        
        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .WithMessage("License number is required");
    }
}
