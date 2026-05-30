

using static SchoolProject.Core.Features.Student.Queries.Response.GetStudentByIdResponse;

namespace SchoolProject.Core.Features.Department.Queries.Response
{
    public partial class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string DName { get; set; }
        public InstructorIdAndName InstructorManager { get; set; }
        public ICollection<StudentIdAndName> Students { get; set; }
        public ICollection<InstructorIdAndName> Instructors { get; set; }
        public ICollection<DepartmentSubjectsName>? DepartmentSubjects { get; set; }

    }

}

