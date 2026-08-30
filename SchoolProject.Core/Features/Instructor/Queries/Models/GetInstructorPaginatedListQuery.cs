
using MediatR;
using SchoolProject.Core.Features.Instructor.Queries.Response;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Enums;

namespace SchoolProject.Core.Features.Instructor.Queries.Models
{
    public class GetInstructorPaginatedListQuery:IRequest<PaginatedRasult<GetInstructorPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public InstructorOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
