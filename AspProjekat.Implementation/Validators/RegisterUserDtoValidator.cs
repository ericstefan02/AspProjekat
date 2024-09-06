using AspProjekat.Application.DTO;
using AspProjekat.DataAccess;
using FluentValidation;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    private readonly AspContext _ctx;

    public RegisterUserDtoValidator(AspContext ctx)
    {
        _ctx = ctx;
        CascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(x => !_ctx.Users.Any(u => u.Email == x))
            .WithMessage("Email is already in use.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")
            .WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter and one number.");

        RuleFor(x => x.Username)
            .NotEmpty()
            .Matches("(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
            .WithMessage("Invalid username format.")
            .Must(x => !_ctx.Users.Any(u => u.Username == x))
            .WithMessage("Username is already in use.");

        RuleFor(x => x.RoleId)
            .Must(roleId => _ctx.Roles.Any(r => r.Id == roleId))
            .WithMessage("Role does not exist.");
    }
}
