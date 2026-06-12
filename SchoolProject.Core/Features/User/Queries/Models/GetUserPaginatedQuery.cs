

using MediatR;
using SchoolProject.Core.Features.User.Queries.Response;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.User.Queries.Models
{
    public class GetUserPaginatedQuery:IRequest<PaginatedRasult<GetUserPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
