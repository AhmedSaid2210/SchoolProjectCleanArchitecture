

using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Department.Commands.Models
{
    public class DeleteDepartmentCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }

    }
}
