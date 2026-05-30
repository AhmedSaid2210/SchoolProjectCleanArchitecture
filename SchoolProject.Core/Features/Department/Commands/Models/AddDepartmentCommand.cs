

using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Department.Commands.Models
{
    public class AddDepartmentCommand :IRequest<Response<string>>
    {
        public string DName { get; set; }

        public int? InstructorManagerID { get; set; }

    }
}
