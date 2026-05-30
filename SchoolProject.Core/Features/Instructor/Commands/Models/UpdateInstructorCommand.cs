

using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructor.Commands.Models
{
    public class UpdateInstructorCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentID { get; set; }
        public int? SupervisorID { get; set; }
    }
}
