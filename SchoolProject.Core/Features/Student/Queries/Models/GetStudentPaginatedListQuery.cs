

using MediatR;
using SchoolProject.Core.Features.Student.Queries.Response;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Features.Student.Queries.Models
{
    public class GetStudentPaginatedListQuery:IRequest<PaginatedRasult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
