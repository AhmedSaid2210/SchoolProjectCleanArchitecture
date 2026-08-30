

using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator:AbstractValidator<EditRoleCommand>
    {
        private readonly IAuthorizationService _authorizationService;
        public EditRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyCustomValidation();
            ApplyCustomValidationForRoleName();
        }

        private void ApplyCustomValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID Is Empty")
                .NotNull().WithMessage("ID Not Null");
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Name Is Empty")
                .NotNull().WithMessage("Name Is Null");
        }
        private void ApplyCustomValidationForRoleName()
        {
            RuleFor(x => x.RoleName).MustAsync(async (roleName, cancellationToken) =>
            {
                return ! (await _authorizationService.IsRoleExistsAsync(roleName)); 
            }).WithMessage("Role name already exists.");
        }
    }
}
