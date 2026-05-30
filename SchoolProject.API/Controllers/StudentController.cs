using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Features.Student.Queries.Models;
using SchoolProject.Data.AppMetaData;
using SchoolProject.Data.Entities;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class StudentController : AppControllerBase
    {
    

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetAllStudents()
        {
            return NewResult(await _mediator.Send(new GetStudentsListQuery()));
        }
        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> GetPaginatedStudents([FromQuery]GetStudentPaginatedListQuery query)
        {
            return Ok(await _mediator.Send(query) );
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentBuId([FromRoute]int id)
        {
            return NewResult( await _mediator.Send(new GetStudentByIdQuery(id) ) );
        }
        [HttpPost(Router.StudentRouting.AddStudent)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpPut(Router.StudentRouting.Update)]
        public async Task<IActionResult> AddStudent([FromBody] UpdateStudentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new DeleteStudentCommand(id)));
        }
    }
}
