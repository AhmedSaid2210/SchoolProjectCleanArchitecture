

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Queries.Response;

namespace SchoolProject.Core.Features.Instructor.Queries.Models
{
    public class GetInstructorByIdQuery:IRequest<Response<GetInstructorByIdResponse>>
    {
        public GetInstructorByIdQuery()
        {
            
        }
        public GetInstructorByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
