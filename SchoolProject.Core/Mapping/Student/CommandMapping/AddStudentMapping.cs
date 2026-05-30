using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Features.Student.Queries.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Mapping.Student
{
    public partial class StudentProfile
    {
        public void AddStudentMapping()
        {
            CreateMap<AddStudentCommand, Data.Entities.Student>()
                .ForMember(dest => dest.DID,opt=>opt.MapFrom(src=>src.DepartmentID));  
        }
    }
}
