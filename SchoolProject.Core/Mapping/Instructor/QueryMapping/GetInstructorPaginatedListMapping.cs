

using SchoolProject.Core.Features.Department.Queries.Response;
using SchoolProject.Core.Features.Instructor.Queries.Response;
using SchoolProject.Core.Features.Student.Queries.Response;
using static SchoolProject.Core.Features.Instructor.Queries.Response.GetAllInstructorResponse;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile
    {
       public void GetInstructorPaginatedListMapping()
       {
            CreateMap<Data.Entities.Instructor, GetInstructorPaginatedListResponse>()
               .ForMember(dest => dest.Department, opt => opt.MapFrom(src => new DepartmentIdAndName(src.Department.Id, src.Department.DName)))
               .ForMember(dest => dest.DepartmentManger, opt => opt.MapFrom(src => new DepartmentIdAndName(src.DepartmentManger.Id, src.DepartmentManger.DName)))
               .ForMember(dest => dest.InstructorSupervisor, opt => opt.MapFrom(src => new InstructorIdAndName(src.InstructorSupervisor.Id, src.InstructorSupervisor.Name)))
               .ForMember(dest => dest.InstructorsSupervised, opt => opt.MapFrom(src => src.Instructors.Where(i => i.IsDeleted.Equals(false)).Select(i => new InstructorIdAndName(i.Id, i.Name)).ToList()))
           ;
        }
    }
}
