using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Instructor.Commands.Models;
using SchoolProject.Core.Features.Instructor.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRouting.List)]
        public async Task<IActionResult> GetAllInstructors()
        {
            return NewResult(await _mediator.Send(new GetAllInstructorQuery()));
        }
        [HttpGet(Router.InstructorRouting.Paginated)]
        public async Task<IActionResult> GetAllInstructorsPaginated([FromQuery]GetInstructorPaginatedListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet(Router.InstructorRouting.GetById)]
        public async Task<IActionResult> GetInstructorById([FromRoute]int id)
        {
            return NewResult(await _mediator.Send(new GetInstructorByIdQuery(id)));
        }
        [HttpPost(Router.InstructorRouting.AddInstructor)]
        public async Task<IActionResult> AddInstructor(AddInstructorCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpPut(Router.InstructorRouting.Update)]
        public async Task<IActionResult> UpdateInstructor(UpdateInstructorCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpDelete(Router.InstructorRouting.Delete)]
        public async Task<IActionResult> DeletedById([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new DeleteInstructorByIdCommand
            {
                Id = id 
            }));
        }
    }
}
