

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Queries.Response;

namespace SchoolProject.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery:IRequest<Response< GetUserByIdResponse>>
    {
        public int Id { get; set; }
    }
}
