

using MediatR;
using SchoolProject.Core.Features.Department.Queries.Response;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Enums;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetDepartmentPaginatedListQuery:IRequest<PaginatedRasult<GetDepartmentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DepartmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
