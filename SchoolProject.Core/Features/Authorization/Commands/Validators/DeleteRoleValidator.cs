

using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator:AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            ApplyCustomValidation();
        }
        private void ApplyCustomValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Not Empty")
                .NotNull().WithMessage("ID Not Null")
                ;
        }
    }
}
