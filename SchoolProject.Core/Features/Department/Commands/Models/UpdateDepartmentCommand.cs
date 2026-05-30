

using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Department.Commands.Models
{
    public class UpdateDepartmentCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string DName { get; set; }
        public int? InstructorManagerID { get; set; }
    }
}
