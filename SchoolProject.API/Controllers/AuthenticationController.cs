using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
           return NewResult(await _mediator.Send(command));
        }
        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromQuery] RefreshTokenCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpGet(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery command)
        {
            return NewResult(await _mediator.Send(command));
        }
    }
}
