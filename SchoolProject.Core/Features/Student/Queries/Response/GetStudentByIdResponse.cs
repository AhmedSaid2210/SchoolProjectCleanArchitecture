

namespace SchoolProject.Core.Features.Student.Queries.Response
{
    public class GetStudentByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? DepartmentName { get; set; }
        public  ICollection<StudentSubjectAndGrade>? StudentSubjects { get; set; }

        public class StudentSubjectAndGrade
        {
            public string? SubjectName { get; set; }
            public decimal? Grade { get; set; }
          
        }
    }
}
