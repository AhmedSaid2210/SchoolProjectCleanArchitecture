
using SchoolProject.Core.Features.Department.Queries.Response;
using SchoolProject.Data.Entities;
using static SchoolProject.Core.Features.Department.Queries.Response.GetDepartmentByIdResponse;

namespace SchoolProject.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
      public void GetDepartmentByIdMapping()
      {
            CreateMap<Data.Entities.Department, GetDepartmentByIdResponse>()
                .ForMember(dest => dest.Students , opt => opt.MapFrom(src=>src.Students.Select(s => new StudentIdAndName { Id = s.Id , StudentsName = s.Name }).ToList()))
                .ForMember(dest => dest.InstructorManager , opt => opt.MapFrom(src => new InstructorIdAndName { Id = src.InstructorManager.Id,InstructorName = src.InstructorManager.Name}) )
                .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors.Select(s => new InstructorIdAndName { Id = s.Id, InstructorName = s.Name }).ToList()))
                .ForMember(dest => dest.DepartmentSubjects, opt => opt.MapFrom(src => src.DepartmentSubjects.Select(s => new DepartmentSubjectsName(s.Subjects.SubjectName)  ).ToList()));
      }
    }
}
