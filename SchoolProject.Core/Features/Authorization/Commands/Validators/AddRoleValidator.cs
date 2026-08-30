

using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator :AbstractValidator<AddRoleCommand>
    {
        private readonly IAuthorizationService _authorizationService;
        public AddRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;


            ApplyValidation();
            ApplyCustomValidation();

        }

        public void ApplyValidation()
        {
            RuleFor(x => x.RoleName)
               .NotEmpty().WithMessage("Role name is required.")
               .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");
        }

        public void ApplyCustomValidation()
        {
            RuleFor(x => x.RoleName).MustAsync(async (roleName, cancellation) =>

                 // Check if the role already exists in the database
                 // Replace this with your actual database check logic
                 !await _authorizationService.IsRoleExistsAsync(roleName)

            ).WithMessage("Role name already exists.");
        }
}
}
