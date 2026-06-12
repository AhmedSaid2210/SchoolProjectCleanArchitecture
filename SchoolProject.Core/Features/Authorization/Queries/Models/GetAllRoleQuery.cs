

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Response;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class GetAllRoleQuery : IRequest<Response<List<GetAllRoleResponse>>>
    {
    }
}
