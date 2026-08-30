

using FluentValidation;
using SchoolProject.Core.Features.User.Commands.Models;

namespace SchoolProject.Core.Features.User.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

            RuleFor(x=>x.ConfirmPassword).NotEmpty().WithMessage("Password is required")
                .Equal(x=>x.Password).WithMessage("ConfirmPassword must be Equal Password")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

        }
    }
}
