using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost(Router.UserRouting.AddUser)]
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            return NewResult(await _mediator.Send(command) );
        }
    }
}
