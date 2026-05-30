

using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructor.Commands.Models
{
    public class DeleteInstructorByIdCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
