using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Features.User.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> GetUserPaginated([FromQuery] GetUserPaginatedQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetUserById(int id)
        {
            return NewResult(await _mediator.Send(new GetUserByIdQuery { Id = id}));
        }

        [HttpPost(Router.UserRouting.AddUser)]
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            return NewResult(await _mediator.Send(command) );
        }

        [HttpPut(Router.UserRouting.Update)]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpPut(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return NewResult(await _mediator.Send(new DeleteUserCommand { Id = id }));
        }
    }
}
