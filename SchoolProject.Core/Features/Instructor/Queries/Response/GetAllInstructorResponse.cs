

namespace SchoolProject.Core.Features.Instructor.Queries.Response
{
    using SchoolProject.Core.Features.Department.Queries.Response;

    public class GetAllInstructorResponse
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

        public class DepartmentIdAndName
        {
            public DepartmentIdAndName()
            {
                
            }
            public DepartmentIdAndName(int id,string name)
            {
                Id = id;
                DepartmentName = name;
            }
            public int Id { get; set; }
            public string DepartmentName { get; set; }
        }
    }
}
