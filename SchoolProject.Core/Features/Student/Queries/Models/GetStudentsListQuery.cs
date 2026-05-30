using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.Response;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Student.Queries.Models
{
    public class GetStudentsListQuery:IRequest<Response< List<GetStudentsListResponse>>>
    {
        

    }
}
