using SchoolProject.Core.Features.Student.Queries.Response;
using static SchoolProject.Core.Features.Student.Queries.Response.GetStudentByIdResponse;


namespace SchoolProject.Core.Mapping.Student
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
     
            CreateMap<Data.Entities.Student, GetStudentByIdResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DName))
                .ForMember(dest => dest.StudentSubjects, opt => opt.MapFrom(src => src.StudentsSubjects.Select(s => new StudentSubjectAndGrade { SubjectName = s.Subject == null ? "Student Not Have Subject" : s.Subject.SubjectName, Grade = s.Grade } ).ToList()))
                ;
        }
    }
}
