

using SchoolProject.Core.Features.Department.Queries.Response;
using SchoolProject.Data.Entities;
using static SchoolProject.Core.Features.Instructor.Queries.Response.GetAllInstructorResponse;

namespace SchoolProject.Core.Features.Instructor.Queries.Response
{
    public class GetInstructorByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }


        public DepartmentIdAndName? Department { get; set; }

        public DepartmentIdAndName? DepartmentManger { get; set; }

        public InstructorIdAndName? InstructorSupervisor { get; set; }
        public ICollection<InstructorIdAndName>? InstructorsSupervised { get; set; }
        public  ICollection<InstructorSubjectName> InstructorSubjects { get; set; }


    }
}
