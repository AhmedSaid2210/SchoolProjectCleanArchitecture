

namespace SchoolProject.Core.Features.Department.Queries.Response
{
    public class GetDepartmentPaginatedListResponse
    {
        public int Id { get; set; }
        public string DName { get; set; }
        public InstructorIdAndName InstructorManager { get; set; }
        public ICollection<StudentIdAndName> Students { get; set; }
        public ICollection<InstructorIdAndName> Instructors { get; set; }
    }
}
