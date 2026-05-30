using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Student.Commands.Models
{
    public class DeleteStudentCommand: IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public DeleteStudentCommand(int id)
        {
            Id = id;   
        }
    }
}
