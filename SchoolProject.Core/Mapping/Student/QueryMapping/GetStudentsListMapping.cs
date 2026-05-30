using SchoolProject.Core.Features.Student.Queries.Response;


namespace SchoolProject.Core.Mapping.Student
{
    public partial class StudentProfile
    {
        public void GetStudentsListMapping()
        {
            CreateMap<Data.Entities.Student, GetStudentsListResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DName));
        }
             
    }
}
