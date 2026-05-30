

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Response;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetAllDepartmentQuery:IRequest<Response<List<GetAllDepartmentResponse>>>
    {
    }
}
