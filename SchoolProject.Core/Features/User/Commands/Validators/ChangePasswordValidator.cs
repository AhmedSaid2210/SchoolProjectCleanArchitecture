
using FluentValidation;
using SchoolProject.Core.Features.User.Commands.Models;

namespace SchoolProject.Core.Features.User.Commands.Validators
{
    public class ChangePasswordValidator :AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            ApplyCustomValidation();
        }

        private void ApplyCustomValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID Is Empty")
                .NotNull().WithMessage("ID Not Null");

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("NewPassword Is Empty")
                .NotNull().WithMessage("NewPassword Is Null");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword Is Empty")
                .Equal(x => x.NewPassword)
                .NotNull().WithMessage("ConfirmPassword Is Null");

            
        }
    }
}
