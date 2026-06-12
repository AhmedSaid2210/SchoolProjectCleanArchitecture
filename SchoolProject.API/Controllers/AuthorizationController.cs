using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthorizationController : AppControllerBase
    {
        [HttpGet(Router.AuthorizationRouting.List)]
        public async Task<IActionResult> GetAllRoles()
        {
            var command = new GetAllRoleQuery();
            return NewResult(await _mediator.Send(command));
        }
        [HttpGet(Router.AuthorizationRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var command = new GetRoleByIdQuery { Id = id };
            return NewResult(await _mediator.Send(command));
        }

        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRole([FromQuery] AddRoleCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpPut(Router.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromQuery] EditRoleCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromQuery] DeleteRoleCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
    }
}
