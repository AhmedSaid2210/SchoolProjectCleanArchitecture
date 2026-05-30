using SchoolProject.Core.Features.Student.Commands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Mapping.Student
{
    public partial class StudentProfile
    {
        public void UpdateStudentMapping()
        {
            CreateMap<UpdateStudentCommand, Data.Entities.Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentID));
        }
    }
}
