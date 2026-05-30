

using FluentValidation;
using SchoolProject.Core.Features.Department.Commands.Models;

namespace SchoolProject.Core.Features.Department.Commands.Validatiors
{
    public class AddDepartmentValidator:AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentValidator()
        {
            RuleFor(x => x.DName)
                .NotNull().WithMessage("Department Name Not Be NUll")
                .NotEmpty().WithMessage("Department Name Not Be Empty")
                .MaximumLength(100);
        }
    }
}
