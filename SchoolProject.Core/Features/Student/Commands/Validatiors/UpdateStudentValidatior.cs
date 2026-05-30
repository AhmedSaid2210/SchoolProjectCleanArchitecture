using FluentValidation;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Student.Commands.Validatiors
{
    public class UpdateStudentValidatior: AbstractValidator<UpdateStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructor
        public UpdateStudentValidatior(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationsRules();
        }
        #endregion

        #region Handles Functions 
        public void ApplyValidationsRules()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Name Must Not Be Empty")
                .NotNull().WithMessage("Name Must Not Be Null")
                .MaximumLength(20).WithMessage("Max Length is 20");
            RuleFor(s => s.Address)
             .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
             .NotNull().WithMessage("{PropertyValue} Must Not Be Null")
             .MaximumLength(50).WithMessage("{PropertyName} Length is 50");
        }
        public void ApplyValidationsCustomRules()
        {
            RuleFor(s => s.Name).MustAsync(async (model,key, CancellationToken) => !await _studentService.IsNameExsitExcludeSelf(key, model.Id))
                .WithMessage("Name is exist");
        }
        #endregion
    }
}
