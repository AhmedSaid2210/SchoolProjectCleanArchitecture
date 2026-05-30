

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Queries.Response;


namespace SchoolProject.Core.Features.Instructor.Queries.Models
{
    public class GetAllInstructorQuery : IRequest<Response<List<GetAllInstructorResponse>>>
    {
        
    }
}
