using AuthenticationPoc.DataTransferObjects;
using FluentValidation;

namespace AuthenticationPoc.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).Must(x => x.Contains('@')).WithMessage("Your password must contain a @ character!");
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.Role).Must(x => Equals(x, "Admin") || Equals(x, "Seller") || Equals(x, "User"));
        }
    }
}
