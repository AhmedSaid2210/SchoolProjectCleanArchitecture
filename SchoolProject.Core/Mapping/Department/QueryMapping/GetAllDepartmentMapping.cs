
using SchoolProject.Core.Features.Department.Queries.Response;


namespace SchoolProject.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
        public void GetAllDepartmentMapping()
        {
            CreateMap<Data.Entities.Department, GetAllDepartmentResponse>()
                .ForMember(dest=>dest.Students, opt=>opt.MapFrom(src => src.Students.Select(s=>new StudentIdAndName { Id = s.Id,StudentsName=s.Name}).ToList()))
                .ForMember(dest => dest.InstructorManager, opt => opt.MapFrom(src => new InstructorIdAndName { Id = src.InstructorManager.Id, InstructorName = src.InstructorManager.Name }))
                .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors.Select(s => new InstructorIdAndName { Id = s.Id, InstructorName = s.Name }).ToList()));

        }
    }
}
