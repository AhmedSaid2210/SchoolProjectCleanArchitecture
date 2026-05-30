
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Student.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    [ApiController]
    public class DepartmentController : AppControllerBase
    {

        [HttpGet(Router.DepartmentRouting.List)]
        public async Task<IActionResult> GetAllStudents()
        {
            return NewResult(await _mediator.Send(new GetAllDepartmentQuery()));
        }
        [HttpGet(Router.DepartmentRouting.Paginated)]
        public async Task<IActionResult> GetAllPaginatedStudents([FromQuery] GetDepartmentPaginatedListQuery query)
        {
            return Ok(await _mediator.Send(query) );
        }
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetAllStudents([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new GetDepartmentByIdQuery() { Id = id}));
        }

        [HttpPost(Router.DepartmentRouting.AddDepartment)]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpPut(Router.DepartmentRouting.Update)]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpDelete(Router.DepartmentRouting.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new DeleteDepartmentCommand { Id = id }));
        }
    }
}
