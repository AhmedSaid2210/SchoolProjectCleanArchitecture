using AutoMapper;

namespace SchoolProject.Core.Mapping.Student
{
    public partial class StudentProfile:Profile
    {
        public StudentProfile()
        {
            GetStudentsListMapping();
            GetStudentByIdMapping();
            AddStudentMapping();
            UpdateStudentMapping();
 
        }
    }
}
