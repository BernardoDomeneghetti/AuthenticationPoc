using AuthenticationPoc.DataTransferObjects;
using FluentValidation;

namespace AuthenticationPoc.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).Must(x => x.Contains('@')).WithMessage("Your password must contain a @ character!");
        }
    }
}
