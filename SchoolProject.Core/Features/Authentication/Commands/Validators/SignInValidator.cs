
using FluentValidation;
using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator:AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is Empty")
                .NotNull().WithMessage("UserName is Null");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Empty")
                .NotNull().WithMessage("Password is Null");
        }
    }
}
